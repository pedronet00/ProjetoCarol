using ProjetoCarol.Domain.Entities.Usuario;

namespace ProjetoCarol.Domain.Interfaces.Usuario;

public interface IUsuarioPagamentoRepository
{
    Task<UsuarioPagamento> Criar(UsuarioPagamento pagamento);

    Task<UsuarioPagamento> EncontrarPeloId(Guid? id);
}
