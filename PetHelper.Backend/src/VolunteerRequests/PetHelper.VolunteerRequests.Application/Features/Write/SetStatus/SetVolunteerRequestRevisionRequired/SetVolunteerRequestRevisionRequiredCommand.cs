using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestRevisionRequired;
public record SetVolunteerRequestRevisionRequiredCommand(
    Guid VolunteerRequestId, 
    Guid AdminId, 
    string RejectedComment) : ICommand;
