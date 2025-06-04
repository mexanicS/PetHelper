using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;

namespace PetHelper.VolunteerRequests.Contracts;

public interface ICreateVolunteerRequestContract
{
    public Task<VolunteerRequestId> CreateVolunteerRequest(CancellationToken ct);
}
