using ProjetoCarol.Controllers;

namespace ProjetoCarol.Application.ViewModel.Usuario;

public class UsuarioViewModel
{
    public Guid? Id { get; set; }

    public string? NomeCompleto { get; set; }

    public string? Email { get; set; }

    public string? NumeroTelefone { get; set; }
    public DateTime? DataNascimento { get; set; }

    public IEnumerable<UsuarioMatriculaViewModel> Matriculas { get; set; }

    public bool Status { get; set; }
}
