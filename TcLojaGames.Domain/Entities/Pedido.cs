using System;

namespace TcLojaGames.Domain.Entities;

public class Pedido
{
    public int JogoId { get; set; }
    public Jogo Jogo { get; set; } = default!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = default!;

    public DateTime DataCompra { get; set; } = DateTime.UtcNow;
    public decimal Preco { get; set; }
}
