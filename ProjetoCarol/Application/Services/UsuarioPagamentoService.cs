using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjetoCarol.Application.DTO.Usuario;
using ProjetoCarol.Application.Interfaces;
using ProjetoCarol.Application.ViewModel.Usuario;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Enums;
using ProjetoCarol.Domain.Interfaces;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Domain.Notifications;

namespace ProjetoCarol.Application.Services;

public class UsuarioPagamentoService : IUsuarioPagamentoService
{
    private readonly IUsuarioPagamentoRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public UsuarioPagamentoService(IUsuarioPagamentoRepository repo, IMapper mapper, IUnitOfWork uow)
    {
        _repo = repo;
        _mapper = mapper;
        _uow = uow;
    }

    public async Task<DomainNotificationsResult<decimal>> CalcularFaturamentoMesPassado()
    {
        var result = new DomainNotificationsResult<decimal>();

        var faturamento = await _repo.CalcularFaturamentoMesPassado();

        result.Result = faturamento;

        return result;
    }

    public async Task<DomainNotificationsResult<UsuarioPagamentoViewModel>> Criar(UsuarioPagamentoDTO dto)
    {
        var result = new DomainNotificationsResult<UsuarioPagamentoViewModel>();

        var entity = _mapper.Map<UsuarioPagamento>(dto);

        var createdEntity = await _repo.Criar(entity);

        await _uow.Commit();

        result.Result = _mapper.Map<UsuarioPagamentoViewModel>(createdEntity);

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> AlterarStatus(Guid id, StatusPagamento status)
    {
        var result = new DomainNotificationsResult<bool>();

        if (id == Guid.Empty)
        {
            result.Notifications.Add("Id inválido.");
            return result;
        }

        var pagamento = await _repo.EncontrarPeloId(id);

        if (pagamento is null)
        {
            result.Notifications.Add("Pagamento não encontrado.");
            return result;
        }

        pagamento.AlterarStatus(status);
        await _uow.Commit();

        result.Result = true;
        return result;
    }
}
