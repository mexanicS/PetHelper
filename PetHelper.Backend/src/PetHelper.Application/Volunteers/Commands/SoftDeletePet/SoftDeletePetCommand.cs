using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Volunteers.Commands.SoftDeletePet;

public record SoftDeletePetCommand(
    Guid VolunteerId, 
    Guid PetId) : ICommand;