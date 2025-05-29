using Microsoft.EntityFrameworkCore;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;
using PetHelper.VolunteerRequests.Domain;

namespace PetHelper.VolunteerRequests.Infastructure.DataBase.Write.Repositories;

public class VolunteerRequestRepository(VolunteerRequestDbContext context) : IVolunteerRequestRepository
{
    public async Task Add(VolunteerRequest volunteerRequest)
    {
        await context.AddAsync(volunteerRequest);
    }

    public async Task AddRange(IEnumerable<VolunteerRequest> volunteerRequests)
    {
        await context.AddRangeAsync(volunteerRequests);
    }

    public void Remove(VolunteerRequest volunteerRequest)
    {
        context.Remove(volunteerRequest);
    }

    public void RemoveRange(IEnumerable<VolunteerRequest> volunteerRequest)
    {
        context.RemoveRange(volunteerRequest);
    }

    public void Update(VolunteerRequest volunteerRequest)
    {
        context.Update(volunteerRequest);
    }

    public void UpdateRange(IEnumerable<VolunteerRequest> volunteerRequest)
    {
        context.UpdateRange(volunteerRequest);
    }

    public async Task<VolunteerRequest?> GetById(Guid volunteerRequestId, CancellationToken ct)
    {
        var id = VolunteerRequestId.Create(volunteerRequestId).Value;
        
        var volunteerRequest = await context.VolunteerRequests.FirstOrDefaultAsync(vr=>vr.Id == id, ct);
        return volunteerRequest;
    }

    public async Task<IReadOnlyList<VolunteerRequest>> GetByUserId(Guid? userId, CancellationToken ct)
    {
        var volunteerRequest = await context.VolunteerRequests
            .Where(vr => vr.UserId == userId)
            .ToListAsync(ct);
        
        return volunteerRequest;
    }

    public async Task<IReadOnlyList<VolunteerRequest>> GetByAdminId(Guid? adminId, CancellationToken ct)
    {
        var volunteerRequest = await context.VolunteerRequests
            .Where(vr => vr.AdminId == adminId)
            .ToListAsync(ct);
        
        return volunteerRequest;
    }

    public async Task<IReadOnlyList<VolunteerRequest>> GetByStatus(Constants.VolunteerRequestStatus status, CancellationToken ct)
    {
        var volunteerRequest = await context.VolunteerRequests
            .Where(vr => vr.Status == status)
            .ToListAsync(ct);
        
        return volunteerRequest;
    }
}