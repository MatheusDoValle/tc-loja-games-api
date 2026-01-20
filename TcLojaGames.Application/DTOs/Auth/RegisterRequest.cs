using System.ComponentModel.DataAnnotations;

namespace TcLojaGames.Application.DTOs.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres.")]
    public string Password { get; set; } = default!;
}