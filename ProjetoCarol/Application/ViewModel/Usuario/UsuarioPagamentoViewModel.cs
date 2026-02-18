using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Application.ViewModel.Usuario;

public class UsuarioPagamentoViewModel
{
    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }

    public decimal ValorPago { get; set; }

    public DateTime DataPagamento { get; set; }

    public StatusPagamento Status { get; set; }
}
