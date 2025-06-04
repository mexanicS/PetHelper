using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.VolunteerRequests.Application.Features.Write.CreateVolunteerRequest;

public record CreateVolunteerRequestCommand(Guid UserId, string VolunteerInfo) : ICommand; 