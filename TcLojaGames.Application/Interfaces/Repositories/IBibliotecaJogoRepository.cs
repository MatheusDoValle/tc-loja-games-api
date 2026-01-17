using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Application.Interfaces;

public interface IBibliotecaJogoRepository
{
    Task<bool> ExistsAsync(Guid userId, Guid jogoId, CancellationToken ct);
    Task AddAsync(BibliotecaJogo item, CancellationToken ct);
    Task<bool> RemoveAsync(Guid userId, Guid jogoId, CancellationToken ct);
    Task<IReadOnlyList<Jogo>> GetJogosDoUsuarioAsync(Guid userId, CancellationToken ct);
}