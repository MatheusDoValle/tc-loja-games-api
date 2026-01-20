namespace TcLojaGames.Domain.Entities;

public class BibliotecaJogo : EntidadeBase
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    
    public Guid JogoId { get; set; }
    public Jogo Jogo { get; set; } = default!;
}