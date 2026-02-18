using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;

namespace ProjetoCarol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _userService;

    public UsuarioController(IUsuarioService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UsuarioDTO userDTO)
    {
        var result = await _userService.Criar(userDTO);
        if (result.HasNotifications)
        {
            return BadRequest(result.Notifications);
        }
        return Ok(result.Result);
    }

    [HttpGet("alunos")]
    public async Task<IActionResult> GetAlunos()
    {
        var result = await _userService.ListarAlunos();
        if (result.HasNotifications)
        {
            return BadRequest(result.Notifications);
        }
        return Ok(result.Result);

    }

    [HttpGet("alunos/{id}")]
    public async Task<IActionResult> GetAlunoDetails(Guid id)
    {
        var result = await _userService.DetalhesAluno(id);
        if (result.HasNotifications)
        {
            return BadRequest(result.Notifications);
        }
        return Ok(result.Result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] AtualizarUsuarioDTO atualizarUsuarioDTO)
    {
        var result = await _userService.Atualizar(atualizarUsuarioDTO);
        if (result.HasNotifications)
        {
            return BadRequest(result.Notifications);
        }
        return Ok(result.Result);
    }

    [HttpPatch("{id}/alterarStatus")]
    public async Task<IActionResult> AlterarStatusUsuario(Guid id)
    {
        var result = await _userService.AlterarStatus(id);
        if (result.HasNotifications)
        {
            return BadRequest(result.Notifications);
        }
        return Ok(result.Result);
    }
}
