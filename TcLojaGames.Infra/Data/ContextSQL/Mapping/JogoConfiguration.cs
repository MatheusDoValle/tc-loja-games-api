using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Infra.Data.Context.Sql.Mapping;

public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
{
    public void Configure(EntityTypeBuilder<Jogo> builder)
    {
        builder.ToTable("Jogos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Descricao)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Genero)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Preco)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.DataCadastro)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.UpdatedAtUtc);

        builder.HasMany(x => x.BibliotecaJogos)
            .WithOne(x => x.Jogo)
            .HasForeignKey(x => x.JogoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}