using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Infrastructure.Context;

namespace ProjetoCarol.Infrastructure.Repositories;

public class UsuarioAulaRepository : IUsuarioAulaRepository
{
    private readonly AppDbContext _context;

    public UsuarioAulaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UsuarioAula> Criar(UsuarioAula usuarioAula)
    {
        await _context.UsuarioAula.AddAsync(usuarioAula);
        return usuarioAula;
    }

    public async Task<UsuarioAula> Detalhes(Guid aulaId)
    {
        return await _context.UsuarioAula
            .Where(x => x.Id == aulaId)
            .Include(x => x.UsuarioMatricula)
                .ThenInclude(x => x.Usuario)
            .FirstAsync();
    }

    public async Task<IEnumerable<UsuarioAula>> Listar()
    {
        return await _context.UsuarioAula
            .Include(x => x.UsuarioMatricula)
                .ThenInclude(x => x.Usuario)
            .ToListAsync();
    }
}
