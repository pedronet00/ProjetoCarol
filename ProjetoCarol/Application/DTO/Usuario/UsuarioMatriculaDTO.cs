using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Application.DTO.Usuario;

public class UsuarioMatriculaDTO
{
    public Guid UsuarioId { get; set; }

    public DateTime DataMatricula { get; set; }

    public Idiomas Idioma { get; set; }

    public int NivelAluno { get; set; }
}
