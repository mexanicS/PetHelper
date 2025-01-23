using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;