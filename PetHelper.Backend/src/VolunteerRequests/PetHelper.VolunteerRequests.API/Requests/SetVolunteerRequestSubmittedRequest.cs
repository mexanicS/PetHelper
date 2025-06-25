using PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestSubmitted;

namespace PetHelper.VolunteerRequests.API.Requests;
public record SetVolunteerRequestSubmittedRequest(Guid VolunteerRequestId, Guid AdminId)
{
    public static implicit operator SetVolunteerRequestSubmittedCommand(SetVolunteerRequestSubmittedRequest request)
        => new(request.VolunteerRequestId, request.AdminId);
}
