using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Domain.Entities.Usuario;

public class UsuarioMatricula
{
    public Guid Id { get; private set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public DateTime DataMatricula { get; set; }

    public Idiomas Idioma { get; set; }

    #region Navigation Properties
    public ICollection<UsuarioAula> Aulas { get; set; } = new List<UsuarioAula>();
    #endregion

    private UsuarioMatricula() { }

    public UsuarioMatricula(Guid usuarioId, DateTime dataMatricula, Idiomas idioma)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        DataMatricula = dataMatricula;
        Idioma = idioma;
    }
}
