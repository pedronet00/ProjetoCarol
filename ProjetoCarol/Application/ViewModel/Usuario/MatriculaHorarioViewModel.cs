namespace ProjetoCarol.Application.ViewModel.Usuario;

public class MatriculaHorarioViewModel
{
    public Guid Id { get; set; }
    public Guid UsuarioMatriculaId { get; set; }
    public string? NomeAluno { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HorarioInicio { get; set; }
    public DateTime VigenteAPartirDe { get; set; }
    public DateTime? VigenteAte { get; set; }
    public bool Ativo => VigenteAte == null || VigenteAte.Value.Date >= DateTime.Today;
}