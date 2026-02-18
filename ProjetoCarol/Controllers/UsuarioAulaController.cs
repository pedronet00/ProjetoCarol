using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;

namespace ProjetoCarol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioAulaController : ControllerBase
{
    private readonly IUsuarioAulaService _service;

    public UsuarioAulaController(IUsuarioAulaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Application.DTO.Usuario.UsuarioAulaDTO dto)
    {
        var result = await _service.Criar(dto);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var result = await _service.Listar();
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var result = await _service.Detalhes(id);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AlterarStatus(Guid id, [FromQuery] Domain.Enums.StatusAula novoStatus)
    {
        var result = await _service.AlterarStatus(id, novoStatus);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarUsuarioAulaDTO dto)
    {
        var result = await _service.Atualizar(dto);

        if (result.Notifications.Any())
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

}
