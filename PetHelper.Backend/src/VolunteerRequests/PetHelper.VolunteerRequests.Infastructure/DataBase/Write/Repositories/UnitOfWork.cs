using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetHelper.Core;

namespace PetHelper.VolunteerRequests.Infastructure.DataBase.Write.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly VolunteerRequestDbContext _dbContex;
    public UnitOfWork(VolunteerRequestDbContext dBContext)
    {
        _dbContex = dBContext;
    }
    public async Task<IDbTransaction> BeginTransaction(CancellationToken ct = default)
    {
        var transaction = await _dbContex.Database.BeginTransactionAsync(ct);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContex.SaveChangesAsync(cancellationToken);
    }
}