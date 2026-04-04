using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Domain.Entities;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Infrastructure.Context;

namespace ProjetoCarol.Infrastructure.Repositories;

public class MatriculaHorarioRepository : IMatriculaHorarioRepository
{
    private readonly AppDbContext _context;

    public MatriculaHorarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MatriculaHorario> Criar(MatriculaHorario horario)
    {
        await _context.MatriculaHorario.AddAsync(horario);
        return horario;
    }

    public async Task<MatriculaHorario?> Detalhes(Guid id)
    {
        return await _context.MatriculaHorario
            .Where(x => x.Id == id)
            .Include(x => x.UsuarioMatricula)
                .ThenInclude(x => x.Usuario)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MatriculaHorario>> ListarPorMatricula(Guid matriculaId)
    {
        return await _context.MatriculaHorario
            .Where(x => x.UsuarioMatriculaId == matriculaId)
            .Include(x => x.UsuarioMatricula)
                .ThenInclude(x => x.Usuario)
            .OrderBy(x => x.DiaSemana)
            .ThenBy(x => x.HorarioInicio)
            .ToListAsync();
    }

    // retorna apenas os horários vigentes em uma data específica
    public async Task<IEnumerable<MatriculaHorario>> ListarAtivos()
    {
        return await _context.MatriculaHorario
            .Where(x => x.VigenteAte == null || x.VigenteAte.Value.Date >= DateTime.Now)
            .ToListAsync();
    }
}