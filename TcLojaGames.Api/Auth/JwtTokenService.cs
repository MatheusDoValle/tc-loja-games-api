using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TcLojaGames.Application.DTOs.Auth;
using TcLojaGames.Application.Interfaces;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Api.Auth;

public class JwtTokenService : ITokenService
{
    private readonly JwtOptions _opts;

    public JwtTokenService(IOptions<JwtOptions> opts) => _opts = opts.Value;

    public AuthResponse Generate(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()), // 👈 add

            new(JwtRegisteredClaimNames.Email, user.Email),
            new("name", user.Name),

            new(ClaimTypes.Role, user.Role.ToString()) // 👈 já está certo
        };
        var expires = DateTime.UtcNow.AddMinutes(_opts.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: _opts.Issuer,
            audience: _opts.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new AuthResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAtUtc = expires
        };
    }
}