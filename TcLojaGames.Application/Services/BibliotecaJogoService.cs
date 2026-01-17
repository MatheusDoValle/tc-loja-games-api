using TcLojaGames.Application.DTOs;
using TcLojaGames.Application.Interfaces;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Application.Services;

public class BibliotecaJogoService : IBibliotecaJogoService
{
    private readonly IBibliotecaJogoRepository _bibliotecaRepo;
    private readonly IJogoRepository _jogoRepo;
    private readonly IUserRepository _userRepo;

    public BibliotecaJogoService(
        IBibliotecaJogoRepository bibliotecaRepo,
        IJogoRepository jogoRepo,
        IUserRepository userRepo)
    {
        _bibliotecaRepo = bibliotecaRepo;
        _jogoRepo = jogoRepo;
        _userRepo = userRepo;
    }

    public async Task AddJogoAoUsuarioAsync(string email, Guid jogoId, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-mail é obrigatório.");

        var normalizedEmail = email.Trim().ToLowerInvariant();

        var user = await _userRepo.GetByEmailAsync(normalizedEmail, ct);
        if (user is null) throw new ArgumentException("Usuário não encontrado.");

        var jogo = await _jogoRepo.GetByIdAsync(jogoId, ct);
        if (jogo is null) throw new ArgumentException("Jogo não encontrado.");

        if (await _bibliotecaRepo.ExistsAsync(user.Id, jogoId, ct))
            throw new ArgumentException("Usuário já possui este jogo na biblioteca.");

        var item = new BibliotecaJogo
        {
            UserId = user.Id,
            JogoId = jogoId
        };

        await _bibliotecaRepo.AddAsync(item, ct);
    }

    public async Task<bool> RemoveJogoDoUsuarioAsync(string email, Guid jogoId, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-mail é obrigatório.");

        var normalizedEmail = email.Trim().ToLowerInvariant();

        var user = await _userRepo.GetByEmailAsync(normalizedEmail, ct);
        if (user is null) throw new ArgumentException("Usuário não encontrado.");

        return await _bibliotecaRepo.RemoveAsync(user.Id, jogoId, ct);
    }

    public async Task<IReadOnlyList<JogoDto>> GetJogosDoUsuarioAsync(string email, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-mail é obrigatório.");

        var normalizedEmail = email.Trim().ToLowerInvariant();

        var user = await _userRepo.GetByEmailAsync(normalizedEmail, ct);
        if (user is null) throw new ArgumentException("Usuário não encontrado.");

        var jogos = await _bibliotecaRepo.GetJogosDoUsuarioAsync(user.Id, ct);

        return jogos.Select(j => new JogoDto
        {
            Id = j.Id,
            CreatedAtUtc = j.CreatedAtUtc,
            UpdatedAtUtc = j.UpdatedAtUtc,
            Descricao = j.Descricao,
            Genero = j.Genero,
            Preco = j.Preco,
            DataCadastro = j.DataCadastro
        }).ToList();
    }
}