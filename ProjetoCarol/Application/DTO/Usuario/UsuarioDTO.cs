using System.ComponentModel.DataAnnotations;

namespace ProjetoCarol.Application.DTO.Usuario;

public class UsuarioDTO
{
    public string? Id { get; set; }
    [Required]
    public string? FullName { get; set; }
    public string? UserName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? PhoneNumber { get; set; }

    public bool Status { get; set; }
}