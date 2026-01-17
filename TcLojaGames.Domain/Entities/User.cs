using TcLojaGames.Domain.Enums;

namespace TcLojaGames.Domain.Entities;

public class User : EntidadeBase
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public UserRole Role { get; private set; } = UserRole.User;
    public bool IsActive { get; private set; } = true;

    public ICollection<BibliotecaJogo> BibliotecaJogos { get; private set; } = new List<BibliotecaJogo>();

    private User() { }

    public User(string name, string email, string passwordHash, UserRole role = UserRole.User)
    {
        Name = name.Trim();
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
        Role = role;
    }

    public void PromoteToAdmin() => Role = UserRole.Admin;
    public void Deactivate() => IsActive = false;
}