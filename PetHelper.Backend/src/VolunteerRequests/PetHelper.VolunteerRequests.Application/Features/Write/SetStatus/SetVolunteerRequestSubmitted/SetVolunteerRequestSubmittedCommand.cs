using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestSubmitted;

public record SetVolunteerRequestSubmittedCommand(Guid VolunteerRequestId, Guid AdminId) : IVolunteerRequestStatusCommand;