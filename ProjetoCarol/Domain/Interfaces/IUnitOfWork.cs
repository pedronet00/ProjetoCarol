using Microsoft.EntityFrameworkCore.Storage;

namespace ProjetoCarol.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();

    public Task Rollback();

    Task<IDbContextTransaction> BeginTransactionAsync();
}
