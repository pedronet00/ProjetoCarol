using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.DTO.Auth;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.Interfaces.Auth;

namespace ProjetoCarol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var resultado = await _authService.Login(dto);

        if (resultado.GetType().GetProperty("Erro") != null)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPost("trocar-senha")]
    public async Task<IActionResult> TrocarSenha([FromBody] TrocarSenhaDTO dto)
    {
        var resultado = await _authService.TrocarSenha(dto);

        if (resultado.GetType().GetProperty("Erro") != null)
            return BadRequest(resultado);

        return Ok(resultado);
    }


}
