using Microsoft.EntityFrameworkCore;
using TcLojaGames.Application.Interfaces;
using TcLojaGames.Domain.Entities;
using TcLojaGames.Infra.Data.Context.Sql.Context;

namespace TcLojaGames.Infra.Data.Repositories;

public class BibliotecaJogoRepository : IBibliotecaJogoRepository
{
    private readonly LojaGamesDbContext _db;

    public BibliotecaJogoRepository(LojaGamesDbContext db) => _db = db;

    public Task<bool> ExistsAsync(Guid userId, Guid jogoId, CancellationToken ct)
        => _db.BibliotecaJogos.AnyAsync(x => x.UserId == userId && x.JogoId == jogoId, ct);

    public async Task AddAsync(BibliotecaJogo item, CancellationToken ct)
    {
        _db.BibliotecaJogos.Add(item);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> RemoveAsync(Guid userId, Guid jogoId, CancellationToken ct)
    {
        var item = await _db.BibliotecaJogos
            .FirstOrDefaultAsync(x => x.UserId == userId && x.JogoId == jogoId, ct);

        if (item is null) return false;

        _db.BibliotecaJogos.Remove(item);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IReadOnlyList<Jogo>> GetJogosDoUsuarioAsync(Guid userId, CancellationToken ct)
    {
        return await _db.BibliotecaJogos
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.Jogo)
            .Select(x => x.Jogo)
            .OrderBy(j => j.Descricao)
            .ToListAsync(ct);
    }
}