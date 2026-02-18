using ProjetoCarol.Application.DTO.Auth;

namespace ProjetoCarol.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<object> Login(LoginDTO dto);

    Task<object> TrocarSenha(TrocarSenhaDTO dto);
}
