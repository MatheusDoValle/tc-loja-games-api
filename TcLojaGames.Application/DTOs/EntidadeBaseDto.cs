namespace TcLojaGames.Application.DTOs;

public abstract class EntidadeBaseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}