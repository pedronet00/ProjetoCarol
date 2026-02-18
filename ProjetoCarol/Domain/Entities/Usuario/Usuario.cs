using Microsoft.AspNetCore.Identity;
using ProjetoCarol.Domain.Enums;

namespace ProjetoCarol.Domain.Entities.Usuario;

public class Usuario : IdentityUser<Guid>
{
    public string? NomeCompleto { get; private set; }

    public DateTime DataNascimento { get; private set; }

    public Roles TipoUsuario { get; set; }

    public bool Ativo { get; private set; }

    public bool SenhaTemporaria { get; private set; }

    #region Navigation Properties
    public ICollection<UsuarioMatricula> Matriculas { get; private set; } = new List<UsuarioMatricula>();
    public ICollection<UsuarioPagamento> Pagamentos { get; private set; } = new List<UsuarioPagamento>();
    #endregion

    private Usuario() { } 

    public Usuario(string nomeCompleto, DateTime dataNascimento)
    {
        Id = Guid.NewGuid();
        NomeCompleto = nomeCompleto;
        DataNascimento = dataNascimento;
        Ativo = true;
        SenhaTemporaria = true;
    }

    public void AlterarStatus()
    {
        var result = this.Ativo == true ? this.Ativo = false : this.Ativo = true;
    }
     
    public void DefinirSenhaTemporaria(bool status)
    {
        this.SenhaTemporaria = status;
    }

    public int CalcularIdade()
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - DataNascimento.Year;

        if (DataNascimento.Date > hoje.AddYears(-idade))
            idade--;

        return idade;
    }
}
