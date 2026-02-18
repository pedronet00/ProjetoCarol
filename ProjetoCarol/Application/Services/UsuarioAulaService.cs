using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Domain.Interfaces;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Domain.Notifications;
using ProjetoCarol.Infrastructure.Context;

public class UsuarioAulaService : IUsuarioAulaService
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _uow;
    private readonly IUsuarioAulaRepository _repo;
    private readonly IMapper _mapper;

    public UsuarioAulaService(AppDbContext context, IUnitOfWork uow, IUsuarioAulaRepository repo, IMapper mapper)
    {
        _context = context;
        _uow = uow;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<DomainNotificationsResult<UsuarioAulaViewModel>> AlterarStatus(Guid? id, StatusAula novoStatus)
    {
        var result = new DomainNotificationsResult<UsuarioAulaViewModel>();

        var aula = await _context.UsuarioAula
            .FirstOrDefaultAsync(x => x.Id == id);

        if (aula == null)
        {
            result.Notifications.Add("Aula não encontrada.");
            return result;
        }

        aula.AlterarStatus(novoStatus);

        await _uow.Commit();

        result.Result = new UsuarioAulaViewModel
        {
            Id = aula.Id,
            UsuarioMatriculaId = aula.UsuarioMatriculaId,
            DataAula = aula.DataAula,
            StatusAula = aula.StatusAula,
            Comentarios = aula.Comentarios
        };

        return result;

    }

    public async Task<DomainNotificationsResult<UsuarioAulaViewModel>> Criar(UsuarioAulaDTO dto)
    {
        var result = new DomainNotificationsResult<UsuarioAulaViewModel>();

        var matricula = await _context.UsuarioMatricula
            .Include(x => x.Usuario)
            .FirstOrDefaultAsync(x => x.Id == dto.UsuarioMatriculaId);

        if (matricula == null)
        {
            result.Notifications.Add("Matrícula não encontrada.");
            return result;
        }

        var aulaExistente = await _context.UsuarioAula
            .AnyAsync(x =>
                x.UsuarioMatriculaId == dto.UsuarioMatriculaId &&
                x.DataAula.Date == dto.DataAula.Date);

        if (aulaExistente)
        {
            result.Notifications.Add("Já existe aula cadastrada para essa data.");
            return result;
        }

        var aula = new UsuarioAula(
            dto.UsuarioMatriculaId,
            dto.DataAula,
            dto.StatusAula,
            dto.Comentarios);

        await _context.UsuarioAula.AddAsync(aula);
        await _uow.Commit();
 
        result.Result = new UsuarioAulaViewModel
        {
            Id = aula.Id,
            UsuarioMatriculaId = aula.UsuarioMatriculaId,
            NomeAluno = matricula.Usuario.NomeCompleto,
            DataAula = aula.DataAula,
            StatusAula = aula.StatusAula,
            Comentarios = aula.Comentarios
        };

        return result;
    }

    public async Task<DomainNotificationsResult<UsuarioAulaViewModel>> Detalhes(Guid aulaId)
    {
        var result = new DomainNotificationsResult<UsuarioAulaViewModel>();

        var aula = await _repo.Detalhes(aulaId);

        if (aula == null)
        {
            result.Notifications.Add("Aula não encontrada.");
            return result;
        }

        result.Result = new UsuarioAulaViewModel
        {
            Id = aula.Id,
            UsuarioMatriculaId = aula.UsuarioMatriculaId,
            NomeAluno = aula.UsuarioMatricula.Usuario.NomeCompleto,
            DataAula = aula.DataAula,
            StatusAula = aula.StatusAula,
            Comentarios = aula.Comentarios
        };

        return result;
    }

    public async Task<DomainNotificationsResult<UsuarioAulaViewModel>> Atualizar(AtualizarUsuarioAulaDTO dto)
    {
        var result = new DomainNotificationsResult<UsuarioAulaViewModel>();

        var aula = await _context.UsuarioAula
            .Include(x => x.UsuarioMatricula)
                .ThenInclude(x => x.Usuario)
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (aula == null)
        {
            result.Notifications.Add("Aula não encontrada.");
            return result;
        }

        if (aula.DataAula.Date != dto.DataAula.Date)
        {
            var aulaExistente = await _context.UsuarioAula
                .AnyAsync(x =>
                    x.UsuarioMatriculaId == aula.UsuarioMatriculaId &&
                    x.DataAula.Date == dto.DataAula.Date &&
                    x.Id != dto.Id);

            if (aulaExistente)
            {
                result.Notifications.Add("Já existe aula cadastrada para essa data.");
                return result;
            }
        }

        aula.Atualizar(dto.DataAula, dto.StatusAula, dto.Comentarios);

        await _uow.Commit();

        result.Result = new UsuarioAulaViewModel
        {
            Id = aula.Id,
            UsuarioMatriculaId = aula.UsuarioMatriculaId,
            NomeAluno = aula.UsuarioMatricula.Usuario.NomeCompleto,
            DataAula = aula.DataAula,
            StatusAula = aula.StatusAula,
            Comentarios = aula.Comentarios
        };

        return result;
    }


    public async Task<DomainNotificationsResult<IEnumerable<UsuarioAulaViewModel>>> Listar()
    {
        var result = new DomainNotificationsResult<IEnumerable<UsuarioAulaViewModel>>();

        var aulas = await _repo.Listar();

        result.Result = _mapper.Map<IEnumerable<UsuarioAulaViewModel>>(aulas);

        return result;
    }
}
