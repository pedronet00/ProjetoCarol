using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Application.ViewModel.Usuario;

public class UsuarioMatriculaViewModel
{
    public Guid UsuarioId { get; set; }
    public string? NomeCompleto { get; set; }

    public Guid MatriculaId { get; set; }

    public DateTime DataMatricula { get; set; }

    public Idiomas? Idioma { get; set; }

    public int Nivel { get; set; }
}
