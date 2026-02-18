using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Domain.Interfaces.Usuario;

public interface IUsuarioMatriculaRepository
{
    Task<IEnumerable<UsuarioMatricula>> Listar();

    Task<IEnumerable<UsuarioMatricula>> ListarPorIdioma(Idiomas idioma);

    Task<UsuarioMatricula> CriarMatricula(UsuarioMatricula matricula);
}
