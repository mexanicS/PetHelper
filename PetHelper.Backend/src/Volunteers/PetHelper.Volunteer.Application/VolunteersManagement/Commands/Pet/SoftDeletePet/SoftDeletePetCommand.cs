using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.SoftDeletePet;

public record SoftDeletePetCommand(
    Guid VolunteerId, 
    Guid PetId) : ICommand;