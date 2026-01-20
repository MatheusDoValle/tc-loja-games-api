using System.ComponentModel.DataAnnotations;

namespace TcLojaGames.Application.DTOs;

public class JogoDto : EntidadeBaseDto
{
    [Required, MaxLength(200)]
    public string Descricao { get; set; } = default!;

    [Required, MaxLength(80)]
    public string Genero { get; set; } = default!;

    [Range(0.01, double.MaxValue)]
    public decimal Preco { get; set; }

    public DateTime DataCadastro { get; set; }
}