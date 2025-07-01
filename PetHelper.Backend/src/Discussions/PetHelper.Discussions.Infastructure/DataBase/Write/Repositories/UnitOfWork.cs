using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetHelper.Core;

namespace PetHelper.Discussions.Infastructure.DataBase.Write.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DiscussionDbContext _dbContext;
    public UnitOfWork(DiscussionDbContext dBContext)
    {
        _dbContext = dBContext;
    }
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}