using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Domain.Interfaces;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Infrastructure.Context;

namespace ProjetoCarol.Application.Services;

public class RotinaService
{
    private readonly IMatriculaHorarioRepository _matriculaHorarioRepo;
    private readonly IUsuarioAulaRepository _aulaRepo;
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _uow;

    public RotinaService(
        IMatriculaHorarioRepository matriculaHorarioRepo,
        IUsuarioAulaRepository aulaRepo,
        AppDbContext context,
        IUnitOfWork uow)
    {
        _matriculaHorarioRepo = matriculaHorarioRepo;
        _aulaRepo = aulaRepo;
        _context = context;
        _uow = uow;
    }

    public async Task GerarAulasPelaMatriculaHorario()
    {
        var hoje = DateTime.Today;
        var limite = hoje.AddDays(15);

        var horariosAtivos = await _matriculaHorarioRepo.ListarAtivos();

        foreach (var horario in horariosAtivos)
        {
            // encontra todas as ocorrências do dia da semana nos próximos 15 dias
            // ex: se hoje é quinta e o horário é terça, pega todas as terças até hoje+15
            var diasAGerar = Enumerable
                .Range(0, 16)
                .Select(offset => hoje.AddDays(offset))
                .Where(d => d.DayOfWeek == horario.DiaSemana)
                .ToList();

            foreach (var dia in diasAGerar)
            {
                var dataAula = dia
                    .AddHours(horario.HorarioInicio.Hour)
                    .AddMinutes(horario.HorarioInicio.Minute);

                // evita duplicidade: não cria se já existe aula nessa matrícula
                // com esse horário nesse dia
                var jaExiste = await _context.UsuarioAula
                    .AnyAsync(x =>
                        x.UsuarioMatriculaId == horario.UsuarioMatriculaId &&
                        x.MatriculaHorarioId == horario.Id &&
                        x.DataAula.Date == dia);

                if (jaExiste) continue;

                var aula = new UsuarioAula(
                    horario.UsuarioMatriculaId,
                    dataAula,
                    StatusAula.Agendada,
                    comentarios: null,
                    matriculaHorarioId: horario.Id);

                await _aulaRepo.Criar(aula);
            }
        }

        await _uow.Commit();
    }
}