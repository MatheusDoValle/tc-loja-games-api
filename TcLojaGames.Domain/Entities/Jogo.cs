using System;
using System.Collections.Generic;

namespace TcLojaGames.Domain.Entities;

public class Jogo
{
    public int Id { get; set; }
    public string Descricao { get; set; } = default!;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public string Genero { get; set; } = default!;
    public decimal Preco { get; set; }

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
