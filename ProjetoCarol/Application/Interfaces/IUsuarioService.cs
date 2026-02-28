using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Notifications;

namespace ProjetoCarol.Application.Interfaces;

public interface IUsuarioService
{
    Task<DomainNotificationsResult<UsuarioViewModel>> Criar(UsuarioDTO userDTO);

    Task<DomainNotificationsResult<Dictionary<string, int>>> ContarAlunosAtivos();

    Task<DomainNotificationsResult<bool>> AlterarStatus(Guid id);

    Task<DomainNotificationsResult<UsuarioViewModel>> Atualizar(AtualizarUsuarioDTO dto);

    Task<DomainNotificationsResult<IEnumerable<UsuarioViewModel>>> ListarAlunos();

    Task<DomainNotificationsResult<UsuarioDetalhes>> DetalhesAluno(Guid? idAluno);
}
