using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Application.DTO.Usuario;

public class AtualizarUsuarioAulaDTO
{
    public Guid Id { get; set; }
    public DateTime DataAula { get; set; }
    public StatusAula StatusAula { get; set; }
    public string? Comentarios { get; set; }
}

