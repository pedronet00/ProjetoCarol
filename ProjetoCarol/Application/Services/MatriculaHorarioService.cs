using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Entities;
using ProjetoCarol.Domain.Interfaces;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Domain.Notifications;
using ProjetoCarol.Infrastructure.Context;

namespace ProjetoCarol.Application.Services;

public class MatriculaHorarioService : IMatriculaHorarioService
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _uow;
    private readonly IMatriculaHorarioRepository _repo;

    public MatriculaHorarioService(AppDbContext context, IUnitOfWork uow, IMatriculaHorarioRepository repo)
    {
        _context = context;
        _uow = uow;
        _repo = repo;
    }

    public async Task<DomainNotificationsResult<MatriculaHorarioViewModel>> Criar(MatriculaHorarioDTO dto)
    {
        var result = new DomainNotificationsResult<MatriculaHorarioViewModel>();

        var matricula = await _context.UsuarioMatricula
            .Include(x => x.Usuario)
            .FirstOrDefaultAsync(x => x.Id == dto.UsuarioMatriculaId);

        if (matricula == null)
        {
            result.Notifications.Add("Matrícula não encontrada.");
            return result;
        }

        // impede duplicidade: mesmo dia da semana já ativo nessa matrícula
        var conflito = await _context.MatriculaHorario
            .AnyAsync(x =>
                x.UsuarioMatriculaId == dto.UsuarioMatriculaId &&
                x.DiaSemana == dto.DiaSemana &&
                x.VigenteAte == null);

        if (conflito)
        {
            result.Notifications.Add($"Já existe um horário ativo para {dto.DiaSemana} nessa matrícula. Encerre o anterior antes de criar um novo.");
            return result;
        }

        var horario = new MatriculaHorario(
            dto.UsuarioMatriculaId,
            dto.DiaSemana,
            dto.HorarioInicio,
            dto.VigenteAPartirDe);

        await _repo.Criar(horario);
        await _uow.Commit();

        result.Result = ToViewModel(horario, matricula.Usuario.NomeCompleto);
        return result;
    }

    public async Task<DomainNotificationsResult<MatriculaHorarioViewModel>> Detalhes(Guid id)
    {
        var result = new DomainNotificationsResult<MatriculaHorarioViewModel>();

        var horario = await _repo.Detalhes(id);

        if (horario == null)
        {
            result.Notifications.Add("Horário não encontrado.");
            return result;
        }

        result.Result = ToViewModel(horario, horario.UsuarioMatricula.Usuario.NomeCompleto);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<MatriculaHorarioViewModel>>> ListarPorMatricula(Guid matriculaId)
    {
        var result = new DomainNotificationsResult<IEnumerable<MatriculaHorarioViewModel>>();

        var matricula = await _context.UsuarioMatricula
            .Include(x => x.Usuario)
            .FirstOrDefaultAsync(x => x.Id == matriculaId);

        if (matricula == null)
        {
            result.Notifications.Add("Matrícula não encontrada.");
            return result;
        }

        var horarios = await _repo.ListarPorMatricula(matriculaId);

        result.Result = horarios.Select(h => ToViewModel(h, matricula.Usuario.NomeCompleto));
        return result;
    }

    public async Task<DomainNotificationsResult<MatriculaHorarioViewModel>> Encerrar(Guid id, EncerrarMatriculaHorarioDTO dto)
    {
        var result = new DomainNotificationsResult<MatriculaHorarioViewModel>();

        var horario = await _repo.Detalhes(id);

        if (horario == null)
        {
            result.Notifications.Add("Horário não encontrado.");
            return result;
        }

        if (horario.VigenteAte != null)
        {
            result.Notifications.Add("Esse horário já foi encerrado.");
            return result;
        }

        horario.Encerrar(dto.VigenteAte);
        await _uow.Commit();

        result.Result = ToViewModel(horario, horario.UsuarioMatricula.Usuario.NomeCompleto);
        return result;
    }

    private static MatriculaHorarioViewModel ToViewModel(MatriculaHorario h, string? nomeAluno) => new()
    {
        Id = h.Id,
        UsuarioMatriculaId = h.UsuarioMatriculaId,
        NomeAluno = nomeAluno,
        DiaSemana = h.DiaSemana,
        HorarioInicio = h.HorarioInicio,
        VigenteAPartirDe = h.VigenteAPartirDe,
        VigenteAte = h.VigenteAte
    };
}