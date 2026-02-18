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

    public async Task<UsuarioPagamento> Criar(UsuarioPagamento pagamento)
    {
        _context.UsuarioPagamento.Add(pagamento);

        return pagamento;
    }
}
