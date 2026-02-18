namespace ProjetoCarol.Application.DTO.Usuario;

public class AtualizarUsuarioDTO
{
    public Guid Id { get; set; }
    public string? NomeCompleto { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DataNascimento { get; set; }
    public bool? Ativo { get; set; }
}

