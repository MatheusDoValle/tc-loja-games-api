using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TcLojaGames.Application.DTOs;
using TcLojaGames.Application.Interfaces;

namespace TcLojaGames.Api.Controllers;

/// <summary>
/// Endpoints administrativos para gerenciar a biblioteca (jogos atribuídos) de usuários.
/// </summary>
[ApiController]
[Route("api/admin/biblioteca")]
[Authorize(Roles = "Admin")]
public class AdminBibliotecaController : ControllerBase
{
    private readonly IBibliotecaJogoService _service;

    public AdminBibliotecaController(IBibliotecaJogoService service) => _service = service;

    /// <summary>
    /// Atribui um jogo à biblioteca de um usuário (por e-mail).
    /// </summary>
    /// <param name="dto">E-mail do usuário e ID do jogo a ser vinculado.</param>
    [HttpPost]
    public async Task<IActionResult> Atribuir([FromBody] VincularJogoUsuarioPorEmailDto dto, CancellationToken ct)
    {
        await _service.AddJogoAoUsuarioAsync(dto.Email, dto.JogoId, ct);
        return NoContent();
    }

    /// <summary>
    /// Lista os jogos da biblioteca de um usuário (por e-mail).
    /// </summary>
    /// <param name="email">E-mail do usuário.</param>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<JogoDto>>> Listar([FromQuery] string email, CancellationToken ct)
        => Ok(await _service.GetJogosDoUsuarioAsync(email, ct));

    /// <summary>
    /// Remove um jogo da biblioteca de um usuário (por e-mail).
    /// </summary>
    /// <param name="email">E-mail do usuário.</param>
    /// <param name="jogoId">ID do jogo.</param>
    [HttpDelete]
    public async Task<IActionResult> Remover([FromQuery] string email, [FromQuery] Guid jogoId, CancellationToken ct)
        => (await _service.RemoveJogoDoUsuarioAsync(email, jogoId, ct)) ? NoContent() : NotFound();
}