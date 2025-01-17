using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Volunteers.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;