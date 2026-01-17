using System.ComponentModel.DataAnnotations;

namespace TcLojaGames.Application.DTOs.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Senha é obrigatória.")]
    public string Password { get; set; } = default!;
}