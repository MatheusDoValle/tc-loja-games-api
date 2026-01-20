using Microsoft.Extensions.Logging;
using TcLojaGames.Application.DTOs.Auth;
using TcLojaGames.Application.Interfaces;
using TcLojaGames.Application.Validation;
using TcLojaGames.Domain.Entities;
using TcLojaGames.Domain.Enums;

namespace TcLojaGames.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokens;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository users,
        ITokenService tokens,
        ILogger<AuthService> logger)
    {
        _users = users;
        _tokens = tokens;
        _logger = logger;
    }

    public async Task RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var name = request.Name.Trim();
        var email = request.Email.Trim().ToLowerInvariant();
        var password = request.Password;
        
        AuthValidation.ValidateStrongPassword(password);

        var existing = await _users.GetByEmailAsync(email, ct);
        if (existing is not null)
            throw new ArgumentException("E-mail já cadastrado.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User(name, email, passwordHash, UserRole.User);

        await _users.AddAsync(user, ct);

        _logger.LogInformation("Usuário cadastrado com sucesso. Email={Email}", email);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var email = request.Email.Trim().ToLowerInvariant();
        var password = request.Password;

        var user = await _users.GetByEmailAsync(email, ct);
        if (user is null)
        {
            _logger.LogWarning("Login inválido (usuário não encontrado). Email={Email}", email);
            throw new ArgumentException("Credenciais inválidas.");
        }

        var ok = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!ok)
        {
            _logger.LogWarning("Login inválido (senha incorreta). Email={Email}", email);
            throw new ArgumentException("Credenciais inválidas.");
        }

        _logger.LogInformation("Login realizado com sucesso. Email={Email} Role={Role}", email, user.Role);

        return _tokens.Generate(user);
    }
}