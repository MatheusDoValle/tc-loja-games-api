using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TcLojaGames.Application.DTOs;
using TcLojaGames.Application.Interfaces;

namespace TcLojaGames.Api.Controllers;

[ApiController]
[Route("api/admin/biblioteca")]
[Authorize(Roles = "Admin")]
public class AdminBibliotecaController : ControllerBase
{
    private readonly IBibliotecaJogoService _service;

    public AdminBibliotecaController(IBibliotecaJogoService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Atribuir([FromBody] VincularJogoUsuarioPorEmailDto dto, CancellationToken ct)
    {
        await _service.AddJogoAoUsuarioAsync(dto.Email, dto.JogoId, ct);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<JogoDto>>> Listar([FromQuery] string email, CancellationToken ct)
        => Ok(await _service.GetJogosDoUsuarioAsync(email, ct));

    [HttpDelete]
    public async Task<IActionResult> Remover([FromQuery] string email, [FromQuery] Guid jogoId, CancellationToken ct)
        => (await _service.RemoveJogoDoUsuarioAsync(email, jogoId, ct)) ? NoContent() : NotFound();
}