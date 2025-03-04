using PetHelper.SharedKernel;

namespace PetHelper.Core.Extensions;

public static class EnumerableExtension
{
    public static IQueryable<T> GetExpiredEntitiesQuery<T>(this IQueryable<T> list, int daysToHardDelete) where T : ISoftDeletable
    {
        return list.Where(s => s.IsDeleted
                        && s.DeletionDate != default
                        && s.DeletionDate.AddDays(daysToHardDelete) < DateTime.UtcNow);
    }
}