using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Domain.Entities.Usuario;

public class UsuarioPagamento
{
    public Guid Id { get; private set; }

    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public decimal ValorPago { get; private set; }

    public DateTime DataPagamento { get; private set; }

    public StatusPagamento Status { get; private set; }

    private UsuarioPagamento() { }

    public UsuarioPagamento(Guid usuarioId, decimal valorPago, DateTime dataPagamento)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        ValorPago = valorPago;
        DataPagamento = dataPagamento;
    }
}
