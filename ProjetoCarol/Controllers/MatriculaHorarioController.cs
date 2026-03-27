using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;

namespace ProjetoCarol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatriculaHorarioController : ControllerBase
{
    private readonly IMatriculaHorarioService _service;

    public MatriculaHorarioController(IMatriculaHorarioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] MatriculaHorarioDTO dto)
    {
        var result = await _service.Criar(dto);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes(Guid id)
    {
        var result = await _service.Detalhes(id);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpGet("matricula/{matriculaId}")]
    public async Task<IActionResult> ListarPorMatricula(Guid matriculaId)
    {
        var result = await _service.ListarPorMatricula(matriculaId);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPatch("{id}/encerrar")]
    public async Task<IActionResult> Encerrar(Guid id, [FromBody] EncerrarMatriculaHorarioDTO dto)
    {
        var result = await _service.Encerrar(id, dto);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }
}