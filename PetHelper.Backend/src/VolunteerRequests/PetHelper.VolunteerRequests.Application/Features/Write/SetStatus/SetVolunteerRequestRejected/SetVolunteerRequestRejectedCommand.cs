using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestRejected;
public record SetVolunteerRequestRejectedCommand(
    Guid VolunteerRequestId, 
    Guid AdminId, 
    string RejectedComment) : ICommand;
