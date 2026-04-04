using ProjetoCarol.Domain.Entities;

namespace ProjetoCarol.Domain.Interfaces.Usuario;

public interface IMatriculaHorarioRepository
{
    Task<MatriculaHorario> Criar(MatriculaHorario horario);
    Task<MatriculaHorario?> Detalhes(Guid id);
    Task<IEnumerable<MatriculaHorario>> ListarPorMatricula(Guid matriculaId);
    Task<IEnumerable<MatriculaHorario>> ListarAtivos();
}