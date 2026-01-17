using Microsoft.EntityFrameworkCore;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Infra.Data.Context.Sql.Context;

public class LojaGamesDbContext : DbContext
{
    public LojaGamesDbContext(DbContextOptions<LojaGamesDbContext> options)
        : base(options) { }

    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<User> Users => Set<User>();
    public DbSet<BibliotecaJogo> BibliotecaJogos => Set<BibliotecaJogo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(LojaGamesDbContext).Assembly);
    }
}