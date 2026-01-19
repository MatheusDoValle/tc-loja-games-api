using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TcLojaGames.Application.DTOs.Auth;
using TcLojaGames.Application.Interfaces;

namespace TcLojaGames.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) => _auth = auth;

    /// <summary>
    /// Registra um novo usuário no sistema.
    /// </summary>
    /// <param name="request">Dados necessários para criação do usuário.</param>
    /// <param name="ct">Token para cancelamento da requisição.</param>
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken ct)
    {
        await _auth.RegisterAsync(request, ct);
        return NoContent();
    }

    /// <summary>
    /// Realiza o login do usuário e retorna o token JWT.
    /// </summary>
    /// <param name="request">Credenciais do usuário.</param>
    /// <param name="ct">Token para cancelamento da requisição.</param>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken ct)
    {
        var token = await _auth.LoginAsync(request, ct);
        return Ok(token);
    }

    /// <summary>
    /// Retorna as informações do usuário autenticado a partir do token JWT.
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Id = User.FindFirstValue(ClaimTypes.NameIdentifier) 
                 ?? User.FindFirstValue("sub"),

            Email = User.FindFirstValue(ClaimTypes.Email) 
                    ?? User.FindFirstValue("email"),

            Name = User.FindFirstValue("name"),

            Role = User.FindFirstValue(ClaimTypes.Role)
        });
    }

    /// <summary>
    /// Endpoint acessível apenas por usuários com a role Admin.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
        => Ok(new
        {
            Message = "Você é Admin e acessou um endpoint protegido por role."
        });
}