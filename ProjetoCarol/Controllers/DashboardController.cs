using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.ViewModel;

namespace ProjetoCarol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IUsuarioPagamentoService _usuarioPagamentoService;

    public DashboardController(IUsuarioService usuarioService, IUsuarioPagamentoService usuarioPagamento)
    {
        _usuarioService = usuarioService;
        _usuarioPagamentoService = usuarioPagamento;
    }

    [HttpGet]
    public async Task<DashboardViewModel> Dashboard()
    {
        var alunosAtivos = await _usuarioService.ContarAlunosAtivos();
        var faturamentoMesPassado = await _usuarioPagamentoService.CalcularFaturamentoMesPassado();

        var dashboardViewModel = new DashboardViewModel
        {
            ContagemAlunosAtivos = alunosAtivos.Result,
            FaturamentoMesPassado = faturamentoMesPassado.Result
        };

        return dashboardViewModel;
    }
}
