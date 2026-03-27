using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Notifications;

namespace ProjetoCarol.Application.Interfaces;

public interface IMatriculaHorarioService
{
    Task<DomainNotificationsResult<MatriculaHorarioViewModel>> Criar(MatriculaHorarioDTO dto);
    Task<DomainNotificationsResult<MatriculaHorarioViewModel>> Detalhes(Guid id);
    Task<DomainNotificationsResult<IEnumerable<MatriculaHorarioViewModel>>> ListarPorMatricula(Guid matriculaId);
    Task<DomainNotificationsResult<MatriculaHorarioViewModel>> Encerrar(Guid id, EncerrarMatriculaHorarioDTO dto);
}