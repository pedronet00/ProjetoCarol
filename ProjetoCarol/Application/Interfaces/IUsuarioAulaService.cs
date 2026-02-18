using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Domain.Notifications;

namespace ProjetoCarol.Application.Interfaces;

public interface IUsuarioAulaService
{
    Task<DomainNotificationsResult<UsuarioAulaViewModel>> Criar(UsuarioAulaDTO dto);

    Task<DomainNotificationsResult<UsuarioAulaViewModel>> AlterarStatus(Guid? id,StatusAula novoStatus);

    Task<DomainNotificationsResult<IEnumerable<UsuarioAulaViewModel>>> Listar();

    Task<DomainNotificationsResult<UsuarioAulaViewModel>> Detalhes(Guid aulaId);

    Task<DomainNotificationsResult<UsuarioAulaViewModel>> Atualizar(AtualizarUsuarioAulaDTO dto);
}
