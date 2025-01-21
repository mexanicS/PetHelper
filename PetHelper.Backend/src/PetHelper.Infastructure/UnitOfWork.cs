using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetHelper.Application.Database;
using PetHelper.Infastructure.DbContexts;

namespace PetHelper.Infastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly WriteDbContext _writeDbContext;

    public UnitOfWork(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default)
    {
        var transaction = await 
            _writeDbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _writeDbContext.SaveChangesAsync(cancellationToken);
    }
}