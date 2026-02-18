using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCarol.Domain.Entities.Usuario;

public class UsuarioMatriculaConfiguration : IEntityTypeConfiguration<UsuarioMatricula>
{
    public void Configure(EntityTypeBuilder<UsuarioMatricula> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Idioma)
            .IsRequired();

        builder.Property(x => x.DataMatricula)
            .IsRequired();

        builder.HasMany(x => x.Aulas)
            .WithOne(x => x.UsuarioMatricula)
            .HasForeignKey(x => x.UsuarioMatriculaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
