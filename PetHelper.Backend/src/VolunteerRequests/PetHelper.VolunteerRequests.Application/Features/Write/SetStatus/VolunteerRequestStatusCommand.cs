using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus;

public interface IVolunteerRequestStatusCommand : ICommand
{
    Guid VolunteerRequestId { get; }
    Guid AdminId { get; }
}