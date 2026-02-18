using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoCarol.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Domain.Enums;

[Route("api/[controller]")]
[ApiController]
public class ArquivoController : ControllerBase
{
    private readonly IArquivoService _service;

    public ArquivoController(IArquivoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ListarPorIdioma([FromQuery] Idiomas idioma)
    {
        var arquivos = await _service.ListarPorIdioma(idioma);
        return Ok(arquivos);
    }


    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, [FromForm] Idiomas idioma)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido.");

        var result = await _service.Upload(file, idioma);

        return Ok(result);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var (arquivo, contentType, nome) = await _service.Download(id);

        return File(arquivo, contentType, nome);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var sucesso = await _service.Excluir(id);

        if (!sucesso)
            return NotFound();

        return NoContent();
    }
}

