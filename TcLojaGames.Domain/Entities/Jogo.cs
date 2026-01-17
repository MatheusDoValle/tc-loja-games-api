namespace TcLojaGames.Domain.Entities;

public class Jogo : EntidadeBase
{
    public string Descricao { get; set; } = default!;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public string Genero { get; set; } = default!;
    public decimal Preco { get; set; }

    public ICollection<BibliotecaJogo> BibliotecaJogos { get; set; } = new List<BibliotecaJogo>();
}