using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCarol.Domain.Entities.Usuario;

public class UsuarioAulaConfiguration : IEntityTypeConfiguration<UsuarioAula>
{
    public void Configure(EntityTypeBuilder<UsuarioAula> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusAula)
            .IsRequired();

        builder.Property(x => x.DataAula)
            .IsRequired();
    }
}
