using Microsoft.EntityFrameworkCore;
using PetHelper.Core.Models;

namespace PetHelper.Core.Extensions;

public static class QueriesExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        
        var items =  await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new PagedList<T>
        {
            Items = items,
            PageSize = pageSize,
            PageNumber = page,
            TotalCount = totalCount
        };
    }
}