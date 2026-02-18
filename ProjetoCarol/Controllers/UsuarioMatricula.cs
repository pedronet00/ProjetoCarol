using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.Interfaces;

namespace ProjetoCarol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioMatricula : ControllerBase
{
    private readonly IUsuarioMatriculaService _usuarioMatriculaService;

    public UsuarioMatricula(IUsuarioMatriculaService usuarioMatriculaService)
    {
        _usuarioMatriculaService = usuarioMatriculaService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var resultado = await _usuarioMatriculaService.Listar();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Application.DTO.Usuario.UsuarioMatriculaDTO dto)
    {
        try
        {
            var resultado = await _usuarioMatriculaService.Criar(dto);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
