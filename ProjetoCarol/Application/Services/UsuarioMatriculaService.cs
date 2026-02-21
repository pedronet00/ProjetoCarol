using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Interfaces;
using ProjetoCarol.Domain.Notifications;
using ProjetoCarol.Infrastructure.Context;

public class UsuarioMatriculaService : IUsuarioMatriculaService
{
    private readonly AppDbContext _context;
    private readonly UserManager<Usuario> _userManager;
    private readonly IUnitOfWork _uow;

    public UsuarioMatriculaService(AppDbContext context, UserManager<Usuario> userManager, IUnitOfWork uow)
    {
        _context = context;
        _userManager = userManager;
        _uow = uow;
    }

    public async Task<DomainNotificationsResult<Guid>> Criar(UsuarioMatriculaDTO dto)
    {
        var result = new DomainNotificationsResult<Guid>();
        var usuarioExiste = await _userManager.Users.AnyAsync(u => u.Id == dto.UsuarioId);

        if (!usuarioExiste)
            throw new Exception("Usuário não encontrado.");

        var usuarioJaMatriculado = await _context.UsuarioMatricula.Where(x => x.UsuarioId == dto.UsuarioId && x.Idioma == dto.Idioma).FirstOrDefaultAsync();

        if(usuarioJaMatriculado != null)
        {
            result.Notifications.Add("Usuário já matriculado nesse idioma.");
            return result;
        }

        var matricula = new UsuarioMatricula(
            dto.UsuarioId,
            dto.DataMatricula,
            dto.Idioma,
            dto.NivelAluno);

        await _context.UsuarioMatricula.AddAsync(matricula);
        await _uow.Commit();

        result.Result = matricula.Id;

        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<UsuarioMatriculaViewModel>>> Listar()
    {
        var result = new DomainNotificationsResult<IEnumerable<UsuarioMatriculaViewModel>>();
        var matriculas = await _context.UsuarioMatricula
            .Include(x => x.Usuario)
            .Select(m => new UsuarioMatriculaViewModel
            {
                MatriculaId = m.Id,
                UsuarioId = m.UsuarioId,
                NomeCompleto = m.Usuario.NomeCompleto,
                Idioma = m.Idioma,
                DataMatricula = m.DataMatricula
            })
            .ToListAsync();
        result.Result = matriculas;
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<UsuarioMatriculaViewModel>>> ListarPorUsuario(Guid usuarioId)
    {
        var result = new DomainNotificationsResult<IEnumerable<UsuarioMatriculaViewModel>>();
        var matriculas = await _context.UsuarioMatricula
            .Where(x => x.UsuarioId == usuarioId)
            .Include(x => x.Usuario)
            .Select(m => new UsuarioMatriculaViewModel
            {
                MatriculaId = m.Id,
                UsuarioId = m.UsuarioId,
                NomeCompleto = m.Usuario.NomeCompleto,
                Idioma = m.Idioma,
                DataMatricula = m.DataMatricula,
                Nivel = m.NivelAluno
            })
            .ToListAsync();
        result.Result = matriculas;
        return result;
    }
}
