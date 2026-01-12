namespace TcLojaGames.Domain.Entities;

public class Pedido
{
    public Guid JogoId { get; set; }
    public Jogo Jogo { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public DateTime DataCompra { get; set; } = DateTime.UtcNow;
    public decimal Preco { get; set; }
}