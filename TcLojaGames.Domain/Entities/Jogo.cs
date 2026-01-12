namespace TcLojaGames.Domain.Entities;

public class Jogo
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Descricao { get; set; } = default!;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public string Genero { get; set; } = default!;
    public decimal Preco { get; set; }

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}