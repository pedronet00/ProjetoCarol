using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Infrastructure.Context;

namespace ProjetoCarol.Infrastructure.Repositories;

public class UsuarioMatriculaRepository : IUsuarioMatriculaRepository
{
    private readonly AppDbContext _context;

    public UsuarioMatriculaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UsuarioMatricula> CriarMatricula(UsuarioMatricula matricula)
    {
        await _context.UsuarioMatricula.AddAsync(matricula);

        return matricula;
    }

    public async Task<IEnumerable<UsuarioMatricula>> Listar()
    {
        return await _context.UsuarioMatricula
            .Include(x => x.Usuario)
            .ToListAsync();
    }

    public Task<IEnumerable<UsuarioMatricula>> ListarPorIdioma(Idiomas idioma)
    {
        return Task.FromResult(_context.UsuarioMatricula
            .Include(x => x.Usuario)
            .Where(x => x.Idioma == idioma)
            .AsEnumerable());
    }
}
