

using ProjetoCarol.Domain.Entities.Usuario;

namespace ProjetoCarol.Domain.Entities;

public class MatriculaHorario
{
    public Guid Id { get; private set; }

    public Guid UsuarioMatriculaId { get; private set; }
    public UsuarioMatricula UsuarioMatricula { get; private set; } = null!;

    public DayOfWeek DiaSemana { get; private set; }   
    public TimeOnly HorarioInicio { get; private set; } 

    public DateTime VigenteAPartirDe { get; private set; }  
    public DateTime? VigenteAte { get; private set; }      

    private MatriculaHorario() { }

    public MatriculaHorario(Guid matriculaId, DayOfWeek diaSemana, TimeOnly horario, DateTime vigenteAPartirDe)
    {
        Id = Guid.NewGuid();
        UsuarioMatriculaId = matriculaId;
        DiaSemana = diaSemana;
        HorarioInicio = horario;
        VigenteAPartirDe = vigenteAPartirDe;
    }

    public void Encerrar(DateTime ate) => VigenteAte = ate;
}
