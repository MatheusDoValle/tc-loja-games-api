using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TcLojaGames.Domain.Entities;
using TcLojaGames.Infra.Persistence;

namespace TcLojaGames.Api.Controllers;

[ApiController]
[Route("api/jogos")]
public class JogosControllerTeste : ControllerBase
{
    private readonly AppDbContext _context;

    public JogosControllerTeste(AppDbContext context) => _context = context;

    [Authorize] // ✅ precisa estar logado para ver jogos (opcional)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var jogos = await _context.Jogos.AsNoTracking().ToListAsync();
        return Ok(jogos);
    }

    public record CreateJogoRequest(string Descricao, string Genero, decimal Preco);

    [Authorize(Roles = "Admin")] // ✅ só admin cria jogo
    [HttpPost]
    public async Task<IActionResult> Create(CreateJogoRequest request)
    {
        var jogo = new Jogo
        {
            Descricao = request.Descricao,
            Genero = request.Genero,
            Preco = request.Preco,
            DataCadastro = DateTime.UtcNow
        };

        _context.Jogos.Add(jogo);
        await _context.SaveChangesAsync();

        return Ok(jogo);
    }
}