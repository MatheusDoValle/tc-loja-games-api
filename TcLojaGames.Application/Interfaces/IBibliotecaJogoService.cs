using TcLojaGames.Application.DTOs;

namespace TcLojaGames.Application.Interfaces;

public interface IBibliotecaJogoService
{
    Task AddJogoAoUsuarioAsync(string email, Guid jogoId, CancellationToken ct);
    Task<bool> RemoveJogoDoUsuarioAsync(string email, Guid jogoId, CancellationToken ct);
    Task<IReadOnlyList<JogoDto>> GetJogosDoUsuarioAsync(string email, CancellationToken ct);
}