namespace PetHelper.Core.Abstractions;

public interface IHardDeleteEntitiesContract
{
    public Task HardDeleteExpiredEntities(CancellationToken ct);
}