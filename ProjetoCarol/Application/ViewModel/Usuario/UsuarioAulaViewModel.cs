using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Application.ViewModel.Usuario;

public class UsuarioAulaViewModel
{
    public Guid Id { get; set; }

    public string? NomeAluno { get; set; }

    public Guid UsuarioMatriculaId { get; set; }

    public Idiomas IdiomaAula { get; set; }

    public string? Comentarios { get; set; }

    public DateTime DataAula { get; set; }

    public StatusAula StatusAula { get; set; }
}
