using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Notifications;

namespace ProjetoCarol.Application.Interfaces;

public interface IUsuarioMatriculaService
{
    Task<DomainNotificationsResult<IEnumerable<UsuarioMatriculaViewModel>>> Listar();

    Task<DomainNotificationsResult<Guid>> Criar(UsuarioMatriculaDTO dto);
}
