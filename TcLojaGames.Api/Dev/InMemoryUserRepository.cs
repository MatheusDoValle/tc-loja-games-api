using TcLojaGames.Application.Interfaces;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Api.Dev;

public class InMemoryUserRepository : IUserRepository
{
    private static readonly List<User> Users = new();

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        => Task.FromResult(Users.FirstOrDefault(u => u.Email == email));

    public Task AddAsync(User user, CancellationToken ct)
    {
        Users.Add(user);
        return Task.CompletedTask;
    }
}