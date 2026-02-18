using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCarol.Domain.Entities.Usuario;

public class UsuarioPagamentoConfiguration : IEntityTypeConfiguration<UsuarioPagamento>
{
    public void Configure(EntityTypeBuilder<UsuarioPagamento> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ValorPago)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.DataPagamento)
            .IsRequired();
    }
}
