using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjetoCarol.Application.DTO.Auth;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.Interfaces.Auth;
using ProjetoCarol.Domain.Entities.Usuario;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCarol.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IUsuarioMatriculaService _matriculaService;

    public AuthService(UserManager<Usuario> userManager, IConfiguration configuration, IUsuarioMatriculaService matriculaService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _matriculaService = matriculaService;
    }

    private async Task<string> GerarJwt(Usuario usuario)
    {
        var jwtKey = _configuration.GetSection("Jwt:Key").Value;
        var jwtIssuer = _configuration.GetSection("Jwt:Issuer").Value;

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new Claim("nome", usuario.NomeCompleto),
        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
        new Claim("tipoUsuario", ((int)usuario.TipoUsuario).ToString())
    };

        if ((int)usuario.TipoUsuario == 2)
        {
            var matriculasResult = await _matriculaService.ListarPorUsuario(usuario.Id);

            var idiomas = matriculasResult.Result?
            .Select(x => x.Idioma.ToString())
            .Distinct()
            .ToList() ?? new List<string>();

            var idiomasJson = System.Text.Json.JsonSerializer.Serialize(idiomas);

            claims.Add(new Claim("matriculas", idiomasJson));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<object> Login(LoginDTO dto)
    {
        var usuario = await _userManager.FindByEmailAsync(dto.Email);

        if (usuario == null)
            return new { Erro = "Usuário ou senha inválidos." };

        var senhaValida = await _userManager.CheckPasswordAsync(usuario, dto.Senha);

        if (!senhaValida)
            return new { Erro = "Usuário ou senha inválidos." };

        if (usuario.SenhaTemporaria)
        {
            return new
            {
                TrocarSenha = true,
                Mensagem = "Senha temporária. É necessário definir uma nova senha."
            };
        }

        var token = GerarJwt(usuario);

        return new
        {
            Token = token,
            Nome = usuario.NomeCompleto,
            Email = usuario.Email,
            TipoUsuario = (int)usuario.TipoUsuario
        };
    }
    
    public async Task<object> TrocarSenha(TrocarSenhaDTO dto)
    {
        var usuario = await _userManager.FindByEmailAsync(dto.Email);

        if (usuario == null)
            return new { Erro = "Usuário não encontrado." };

        var senhaValida = await _userManager.CheckPasswordAsync(usuario, dto.SenhaAtual);

        if (!senhaValida)
            return new { Erro = "Senha atual inválida." };

        var resultado = await _userManager.ChangePasswordAsync(
            usuario,
            dto.SenhaAtual,
            dto.NovaSenha);

        if (!resultado.Succeeded)
        {
            return new
            {
                Erro = resultado.Errors.Select(e => e.Description)
            };
        }

        usuario.DefinirSenhaTemporaria(false);
        await _userManager.UpdateAsync(usuario);

        var token = GerarJwt(usuario);

        return new
        {
            Token = token,
            Nome = usuario.NomeCompleto,
            Email = usuario.Email,
            TipoUsuario = (int)usuario.TipoUsuario
        };
    }

}
