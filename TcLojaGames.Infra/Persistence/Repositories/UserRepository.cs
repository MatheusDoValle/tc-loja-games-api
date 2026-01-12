using Microsoft.EntityFrameworkCore;
using TcLojaGames.Application.Interfaces;
using TcLojaGames.Domain.Entities;
using TcLojaGames.Infra.Persistence;

namespace TcLojaGames.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        => _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task AddAsync(User user, CancellationToken ct)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
    }
}