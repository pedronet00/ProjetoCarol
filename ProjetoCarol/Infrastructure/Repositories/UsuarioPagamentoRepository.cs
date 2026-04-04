using Microsoft.EntityFrameworkCore;
using ProjetoCarol.Domain.Entities.Usuario;
using ProjetoCarol.Domain.Interfaces.Usuario;
using ProjetoCarol.Infrastructure.Context;

namespace ProjetoCarol.Infrastructure.Repositories;

public class UsuarioPagamentoRepository : IUsuarioPagamentoRepository
{
    private readonly AppDbContext _context;

    public UsuarioPagamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> CalcularFaturamentoMesPassado()
    {
        var dataAtual = DateTime.UtcNow;
        var primeiroDiaMesPassado = new DateTime(dataAtual.Year, dataAtual.Month, 1).AddMonths(-1);
        var ultimoDiaMesPassado = new DateTime(dataAtual.Year, dataAtual.Month, 1).AddDays(-1);
        var faturamento = await _context.UsuarioPagamento
            .Where(p => p.DataPagamento >= primeiroDiaMesPassado && p.DataPagamento <= ultimoDiaMesPassado)
            .SumAsync(p => p.ValorPago);
        return faturamento;
    }

    public async Task<UsuarioPagamento> Criar(UsuarioPagamento pagamento)
    {
        _context.UsuarioPagamento.Add(pagamento);

        return pagamento;
    }

    public async Task<UsuarioPagamento> EncontrarPeloId(Guid? id)
    {
        var pagamento = await _context.UsuarioPagamento.Where(x => x.Id == id).FirstOrDefaultAsync();

        return pagamento;
    }
}
