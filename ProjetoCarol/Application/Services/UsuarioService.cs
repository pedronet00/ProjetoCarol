using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Domain.Notifications;

namespace ProjetoCarol.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IMapper _mapper;

    public UsuarioService(UserManager<Usuario> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    string GerarSenhaTemporaria(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Nome inválido para gerar senha.");

        var partes = fullName
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var primeiroNome = partes.First().ToLower();
        var ultimoNome = partes.Length > 1
            ? partes.Last().ToLower()
            : partes.First().ToLower();

        // Primeira letra maiúscula
        primeiroNome = char.ToUpper(primeiroNome[0]) + primeiroNome.Substring(1);

        return $"{primeiroNome}.{ultimoNome}@123";
    }



    public async Task<DomainNotificationsResult<UsuarioViewModel>> Criar(UsuarioDTO userDTO)
    {
        var result = new DomainNotificationsResult<UsuarioViewModel>();      

        var _usuarioJaExiste = await _userManager.FindByEmailAsync(userDTO.Email);

        if (_usuarioJaExiste != null)
        {
            result.Notifications.Add("Já existe um usuário com este email.");
            return result;
        }

        var usuario = new Usuario(userDTO.FullName!, DateTime.Now)
        {
            UserName = userDTO.UserName ?? userDTO.Email,
            Email = userDTO.Email,
            PhoneNumber = userDTO.PhoneNumber,
            TipoUsuario = Domain.Enums.Roles.Aluno,
        };

        var senhaTemporaria = GerarSenhaTemporaria(userDTO.FullName!);

        var createResult = await _userManager.CreateAsync(usuario, senhaTemporaria);

        if (createResult != null)
        {
            if (createResult.Succeeded)
            {
                result.Result = _mapper.Map<UsuarioViewModel>(usuario);
            }
            else
            {
                foreach (var error in createResult.Errors)
                {
                    result.Notifications.Add(error.Description);
                }
            }
        }
        else
        {
            result.Notifications.Add("Ocorreu um erro ao criar o usuário.");
        }
        return result;
    }

    public async Task<DomainNotificationsResult<UsuarioDetalhes>> DetalhesAluno(Guid? idAluno)
    {
        var result = new DomainNotificationsResult<UsuarioDetalhes>();

        if (idAluno is null || idAluno == Guid.Empty)
        {
            result.Notifications.Add("Id do aluno inválido.");
            return result;
        }

        var aluno = await _userManager.Users
            .Where(u => u.Id == idAluno.Value && u.TipoUsuario == Roles.Aluno)
            .Select(u => new UsuarioDetalhes
            {
                Id = u.Id,
                NomeCompleto = u.NomeCompleto,
                Email = u.Email,
                NumeroTelefone = u.PhoneNumber,
                DataNascimento = u.DataNascimento,
                Status = u.Ativo,

                Matriculas = u.Matriculas
                    .Select(m => new UsuarioMatriculaViewModel
                    {
                        UsuarioId = u.Id,
                        NomeCompleto = u.NomeCompleto,
                        DataMatricula = m.DataMatricula,
                        MatriculaId = m.Id,
                        Idioma = m.Idioma
                    })
                    .ToList(),

                Pagamentos = u.Pagamentos
                    .Select(p => new UsuarioPagamentoViewModel
                    {
                        Id = p.Id,
                        UsuarioId = p.UsuarioId,
                        ValorPago = p.ValorPago,
                        DataPagamento = p.DataPagamento,
                        Status = p.Status
                    })
                    .OrderByDescending(x => x.DataPagamento)
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (aluno is null)
        {
            result.Notifications.Add("Aluno não encontrado.");
            return result;
        }

        result.Result = aluno;
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<UsuarioViewModel>>> ListarAlunos()
    {
        var result = new DomainNotificationsResult<IEnumerable<UsuarioViewModel>>();

        var alunos = await _userManager.Users
            .Where(u => u.TipoUsuario == Roles.Aluno)
            .Select(u => new UsuarioViewModel
            {
                Id = u.Id,
                NomeCompleto = u.NomeCompleto,
                Email = u.Email,
                NumeroTelefone = u.PhoneNumber,
                DataNascimento = u.DataNascimento,
                Status = u.Ativo,
                Matriculas = u.Matriculas.Select(m => new UsuarioMatriculaViewModel
                {
                    UsuarioId = u.Id,
                    DataMatricula = m.DataMatricula,
                    NomeCompleto = u.NomeCompleto,
                    MatriculaId = m.Id,
                    Idioma = m.Idioma,
                })
            })
    .ToListAsync();


        result.Result = _mapper.Map<IEnumerable<UsuarioViewModel>>(alunos);

        return result;
    }

    public async Task<DomainNotificationsResult<UsuarioViewModel>> Atualizar(AtualizarUsuarioDTO dto)
    {
        var result = new DomainNotificationsResult<UsuarioViewModel>();

        if (dto.Id == Guid.Empty)
        {
            result.Notifications.Add("Id inválido.");
            return result;
        }

        var usuario = await _userManager.FindByIdAsync(dto.Id.ToString());

        if (usuario is null || usuario.TipoUsuario != Roles.Aluno)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        // Mapeia tudo
        _mapper.Map(dto, usuario);

        var updateResult = await _userManager.UpdateAsync(usuario);

        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
                result.Notifications.Add(error.Description);

            return result;
        }

        result.Result = _mapper.Map<UsuarioViewModel>(usuario);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> AlterarStatus(Guid id)
    {
        var result = new DomainNotificationsResult<bool>();

        if (id == Guid.Empty)
        {
            result.Notifications.Add("Id inválido.");
            return result;
        }

        var usuario = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == id && u.TipoUsuario == Roles.Aluno);

        if (usuario is null)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        usuario.AlterarStatus();

        var updateResult = await _userManager.UpdateAsync(usuario);

        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
                result.Notifications.Add(error.Description);

            return result;
        }

        result.Result = true;
        return result;
    }
}
