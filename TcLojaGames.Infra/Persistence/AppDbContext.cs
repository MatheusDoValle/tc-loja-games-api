using Microsoft.EntityFrameworkCore;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Infra.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Jogo>(e =>
        {
            e.ToTable("Jogos");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .ValueGeneratedNever();

            e.Property(x => x.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            e.Property(x => x.Genero)
                .HasMaxLength(80)
                .IsRequired();

            e.Property(x => x.Preco)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            e.Property(x => x.DataCadastro)
                .IsRequired();
        });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .ValueGeneratedNever();

            e.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            e.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            e.HasIndex(x => x.Email).IsUnique();

            e.Property(x => x.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            e.Property(x => x.Role)
                .IsRequired();

            e.Property(x => x.IsActive)
                .IsRequired();

            e.Property(x => x.CreatedAtUtc)
                .IsRequired();
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("Pedidos");

            e.HasKey(x => new { x.JogoId, x.UserId, x.DataCompra });

            e.Property(x => x.Preco)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            e.Property(x => x.DataCompra)
                .IsRequired();

            e.HasOne(x => x.Jogo)
                .WithMany(j => j.Pedidos)
                .HasForeignKey(x => x.JogoId);

            e.HasOne(x => x.User)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(x => x.UserId);
        });
    }
}