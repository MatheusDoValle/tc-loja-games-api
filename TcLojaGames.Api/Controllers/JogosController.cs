using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TcLojaGames.Application.DTOs;
using TcLojaGames.Application.Interfaces;

namespace TcLojaGames.Api.Controllers;

[ApiController]
[Route("api/jogos")]
public class JogosController : ControllerBase
{
    private readonly IJogoService _service;

    public JogosController(IJogoService service) => _service = service;

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<JogoDto>>> GetAll(CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JogoDto>> GetById(Guid id, CancellationToken ct)
    {
        var jogo = await _service.GetByIdAsync(id, ct);
        return jogo is null ? NotFound() : Ok(jogo);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<JogoDto>> Create([FromBody] JogoDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<JogoDto>> Update(Guid id, [FromBody] JogoDto dto, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => (await _service.DeleteAsync(id, ct)) ? NoContent() : NotFound();
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{id:guid}/promocao")]
    public async Task<ActionResult<JogoDto>> AplicarPromocao(Guid id, [FromBody] AplicarPromocaoDto dto, CancellationToken ct)
    {
        var updated = await _service.AplicarPromocaoAsync(id, dto.NovoPreco, ct);
        return updated is null ? NotFound() : Ok(updated);
    }
}