namespace TcLojaGames.Domain.Entities;

public abstract class EntidadeBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; protected set; }

    public void Touch() => UpdatedAtUtc = DateTime.UtcNow;
}