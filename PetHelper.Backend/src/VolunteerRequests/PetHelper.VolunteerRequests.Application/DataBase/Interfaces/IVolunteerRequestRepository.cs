using PetHelper.SharedKernel;
using PetHelper.VolunteerRequests.Domain;

namespace PetHelper.VolunteerRequests.Application.DataBase.Interfaces;

public interface IVolunteerRequestRepository
{
    public Task Add(VolunteerRequest volunteerRequest);

    public Task AddRange(IEnumerable<VolunteerRequest> volunteerRequests);

    public void Remove(VolunteerRequest volunteerRequest);

    public void RemoveRange(IEnumerable<VolunteerRequest> volunteerRequest);

    public void Update(VolunteerRequest volunteerRequest);

    public void UpdateRange(IEnumerable<VolunteerRequest> volunteerRequest);

    public Task<VolunteerRequest?> GetById(Guid volunteerRequestId, CancellationToken ct);

    public Task<IReadOnlyList<VolunteerRequest>> GetByUserId(Guid? userId, CancellationToken ct);

    public Task<IReadOnlyList<VolunteerRequest>> GetByAdminId(Guid? adminId, CancellationToken ct);

    public Task<IReadOnlyList<VolunteerRequest>> GetByStatus(Constants.VolunteerRequestStatus status, CancellationToken ct);
}