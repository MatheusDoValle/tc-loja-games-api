using System.ComponentModel.DataAnnotations;

namespace TcLojaGames.Application.DTOs;

public class VincularJogoUsuarioPorEmailDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public Guid JogoId { get; set; }
}