namespace ProjetoCarol.Application.DTO.Usuario;

public class MatriculaHorarioDTO
{
    public Guid UsuarioMatriculaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HorarioInicio { get; set; }
    public DateTime VigenteAPartirDe { get; set; }
}

public class EncerrarMatriculaHorarioDTO
{
    public DateTime VigenteAte { get; set; }
}