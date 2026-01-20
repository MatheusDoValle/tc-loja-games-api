using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Application.Interfaces;

public interface IJogoRepository
{
    Task AddAsync(Jogo jogo, CancellationToken ct);
    Task<Jogo?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Jogo>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(Jogo jogo, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}