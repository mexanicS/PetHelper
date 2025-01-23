using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.ChangeStatusPet;

public record ChangeStatusPetCommand(
    Guid VolunteerId,
    Guid PetId,
    string Status) : ICommand;