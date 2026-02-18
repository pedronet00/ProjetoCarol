namespace ProjetoCarol.Application.ViewModel.Usuario;

public class UsuarioDetalhes
{
    public Guid? Id { get; set; }

    public string? NomeCompleto { get; set; }

    public string? Email { get; set; }

    public string? NumeroTelefone { get; set; }
    public DateTime? DataNascimento { get; set; }

    public IEnumerable<UsuarioMatriculaViewModel> Matriculas { get; set; }
    public IEnumerable<UsuarioPagamentoViewModel> Pagamentos { get; set; }

    public bool Status { get; set; }
}
