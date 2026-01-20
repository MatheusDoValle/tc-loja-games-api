using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TcLojaGames.Application.DTOs;
using TcLojaGames.Application.Interfaces;

namespace TcLojaGames.Api.Controllers;

[ApiController]
[Route("api/biblioteca")]
[Authorize]
public class BibliotecaController : ControllerBase
{
    private readonly IBibliotecaJogoService _service;

    public BibliotecaController(IBibliotecaJogoService service)
        => _service = service;

    /// <summary>
    /// Retorna a biblioteca de jogos do usuário autenticado.
    /// </summary>
    /// <param name="ct">Token para cancelamento da requisição.</param>
    [HttpGet("me")]
    public async Task<ActionResult<IReadOnlyList<JogoDto>>> MinhaBiblioteca(
        CancellationToken ct)
    {
        var email =
            User.FindFirstValue(ClaimTypes.Email) ??
            User.FindFirstValue("email");

        if (string.IsNullOrWhiteSpace(email))
            return Unauthorized();

        var jogos = await _service.GetJogosDoUsuarioAsync(email, ct);
        return Ok(jogos);
    }
}