using PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestApproved;

namespace PetHelper.VolunteerRequests.API.Requests;
public record SetVolunteerRequestApprovedRequest(Guid VolunteerRequestId, Guid AdminId)
{
    public static implicit operator SetVolunteerRequestApprovedCommand(SetVolunteerRequestApprovedRequest request)
        => new(request.VolunteerRequestId, request.AdminId);
}
