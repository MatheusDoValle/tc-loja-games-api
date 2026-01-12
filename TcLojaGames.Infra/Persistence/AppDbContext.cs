using Microsoft.EntityFrameworkCore;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Infra.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Jogo>(e =>
        {
            e.ToTable("Jogos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Descricao).HasMaxLength(200).IsRequired();
            e.Property(x => x.Genero).HasMaxLength(80).IsRequired();
            e.Property(x => x.Preco).HasColumnType("decimal(18,2)").IsRequired();
            e.Property(x => x.DataCadastro).IsRequired();
        });

        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            e.HasKey(x => x.Id);
            e.Property(x => x.Descricao).HasMaxLength(200).IsRequired();
            e.Property(x => x.Senha).HasMaxLength(255).IsRequired();
            e.Property(x => x.DataCadastro).IsRequired();
            e.Property(x => x.Ativo).IsRequired();
            e.Property(x => x.Adm).IsRequired();
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("Pedidos");
            e.HasKey(x => new { x.JogoId, x.UsuarioId, x.DataCompra });

            e.Property(x => x.Preco).HasColumnType("decimal(18,2)").IsRequired();
            e.Property(x => x.DataCompra).IsRequired();

            e.HasOne(x => x.Jogo)
                .WithMany(j => j.Pedidos)
                .HasForeignKey(x => x.JogoId);

            e.HasOne(x => x.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(x => x.UsuarioId);
        });
    }
}
