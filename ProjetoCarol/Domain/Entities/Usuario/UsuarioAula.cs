using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Domain.Entities.Usuario;

public class UsuarioAula
{
    public Guid Id { get; private set; }

    public Guid UsuarioMatriculaId { get; private set; }
    public UsuarioMatricula UsuarioMatricula { get; private set; } = null!;

    public DateTime DataAula { get; private set; }

    public string? Comentarios { get; private set; }

    public StatusAula StatusAula { get; private set; }

    private UsuarioAula() { }

    public UsuarioAula(Guid usuarioMatriculaId, DateTime dataAula, StatusAula statusAula, string? comentarios)
    {
        Id = Guid.NewGuid();
        UsuarioMatriculaId = usuarioMatriculaId;
        DataAula = dataAula;
        StatusAula = statusAula;
        Comentarios = comentarios;
    }

    public void Atualizar(DateTime dataAula, StatusAula status, string? comentarios)
    {
        DataAula = dataAula;
        StatusAula = status;
        Comentarios = comentarios;
    }


    public void AlterarStatus(StatusAula novoStatus)
        => StatusAula = novoStatus;
}
