using Microsoft.EntityFrameworkCore;
using TcLojaGames.Application.Interfaces;
using TcLojaGames.Domain.Entities;
using TcLojaGames.Infra.Data.Context.Sql.Context;

namespace TcLojaGames.Infra.Data.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly LojaGamesDbContext _db;

    public JogoRepository(LojaGamesDbContext db) => _db = db;

    public async Task AddAsync(Jogo jogo, CancellationToken ct)
    {
        _db.Jogos.Add(jogo);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Jogo?> GetByIdAsync(Guid id, CancellationToken ct)
        => _db.Jogos.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Jogo>> GetAllAsync(CancellationToken ct)
        => await _db.Jogos.AsNoTracking()
            .OrderBy(x => x.Descricao)
            .ToListAsync(ct);

    public async Task UpdateAsync(Jogo jogo, CancellationToken ct)
    {
        _db.Jogos.Update(jogo);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var jogo = await _db.Jogos.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (jogo is null) return false;

        _db.Jogos.Remove(jogo);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}