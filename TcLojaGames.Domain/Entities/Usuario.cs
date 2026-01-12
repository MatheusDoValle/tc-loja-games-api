using System;
using System.Collections.Generic;

namespace TcLojaGames.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Descricao { get; set; } = default!;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // Em produção: preferir hash (SenhaHash). Por enquanto, mantendo como você pediu.
    public string Senha { get; set; } = default!;

    public bool Ativo { get; set; } = true;
    public bool Adm { get; set; } = false;

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
