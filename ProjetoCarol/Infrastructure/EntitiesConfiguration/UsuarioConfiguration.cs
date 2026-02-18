using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCarol.Domain.Entities.Usuario;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.Property(x => x.NomeCompleto)
            .HasMaxLength(150);

        builder.Property(x => x.DataNascimento)
            .IsRequired();

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.HasMany(x => x.Matriculas)
            .WithOne(x => x.Usuario)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Pagamentos)
            .WithOne(x => x.Usuario)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
