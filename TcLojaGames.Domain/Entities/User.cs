using TcLojaGames.Domain.Enums;

namespace TcLojaGames.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public UserRole Role { get; private set; } = UserRole.User;
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    public ICollection<Pedido> Pedidos { get; private set; } = new List<Pedido>();

    private User() { } // EF

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