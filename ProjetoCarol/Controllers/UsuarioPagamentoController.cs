using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;

namespace ProjetoCarol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioPagamentoController : ControllerBase
{
    private readonly IUsuarioPagamentoService _service;

    public UsuarioPagamentoController(IUsuarioPagamentoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] UsuarioPagamentoDTO dto)
    {
        var result = await _service.Criar(dto);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }
}
