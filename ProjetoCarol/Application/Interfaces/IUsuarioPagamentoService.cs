using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Domain.Notifications;

namespace ProjetoCarol.Application.Interfaces;

public interface IUsuarioPagamentoService
{
    Task<DomainNotificationsResult<UsuarioPagamentoViewModel>> Criar(UsuarioPagamentoDTO dto);

    Task<DomainNotificationsResult<bool>> AlterarStatus(Guid id, StatusPagamento status);
}
