using Microsoft.Extensions.DependencyInjection;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.Interfaces.Auth;
using ProjetoCarol.Application.Services;
using ProjetoCarol.Application.Services.Auth;
using ProjetoCarol.Domain.Interfaces;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Infrastructure.Repositories;
using ProjetoCarol.Infrastructure.UnitOfWork;

namespace ProjetoCarol.Crosscutting;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUsuarioMatriculaService, UsuarioMatriculaService>();
        services.AddScoped<IUsuarioAulaService, UsuarioAulaService>();
        services.AddScoped<IUsuarioPagamentoService, UsuarioPagamentoService>();
        services.AddScoped<IArquivoService, ArquivoService>();

        // Repositories
        services.AddScoped<IUsuarioMatriculaRepository, UsuarioMatriculaRepository>();
        services.AddScoped<IUsuarioAulaRepository, UsuarioAulaRepository>();
        services.AddScoped<IUsuarioPagamentoRepository, UsuarioPagamentoRepository>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
