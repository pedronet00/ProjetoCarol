using ProjetoCarol.Domain.Entities.Usuario;

namespace ProjetoCarol.Domain.Interfaces.Usuario;

public interface IUsuarioAulaRepository
{
    Task<IEnumerable<UsuarioAula>> Listar();

    Task<UsuarioAula> Criar(UsuarioAula usuarioAula);

    Task<UsuarioAula> Detalhes(Guid aulaId);
}
