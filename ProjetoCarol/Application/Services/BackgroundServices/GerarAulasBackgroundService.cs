using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoCarol.Application.Services;

namespace ProjetoCarol.Application.BackgroundServices;

public class GerarAulasBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public GerarAulasBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("GerarAulasBackgroundService executando: " + DateTime.Now);
            var agora = DateTime.Now;
            // execuções de hora em hora
            var proxExecucao = agora.Date.AddHours(1);
            var espera = proxExecucao - agora;
            if (espera <= TimeSpan.Zero)
                espera = TimeSpan.FromMinutes(1);

            await Task.Delay(espera, stoppingToken);

            using var scope = _scopeFactory.CreateScope();
            var rotina = scope.ServiceProvider.GetRequiredService<RotinaService>();
            await rotina.GerarAulasPelaMatriculaHorario();
        }
    }
}