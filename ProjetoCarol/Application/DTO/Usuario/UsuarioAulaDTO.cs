using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Application.DTO.Usuario;

public class UsuarioAulaDTO
{
    public Guid Id { get; set; }

    public Guid UsuarioMatriculaId { get; set; }

    public DateTime DataAula { get; set; }

    public string? Comentarios { get; set; }

    public StatusAula StatusAula { get; set; } = StatusAula.Agendada;

}
