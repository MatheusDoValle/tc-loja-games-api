using TcLojaGames.Application.DTOs;

namespace TcLojaGames.Application.Interfaces;

public interface IJogoService
{
    Task<JogoDto> CreateAsync(JogoDto dto, CancellationToken ct);
    Task<JogoDto?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<JogoDto>> GetAllAsync(CancellationToken ct);
    Task<JogoDto?> UpdateAsync(Guid id, JogoDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    Task<JogoDto?> AplicarPromocaoAsync(Guid id, decimal novoPreco, CancellationToken ct);
}