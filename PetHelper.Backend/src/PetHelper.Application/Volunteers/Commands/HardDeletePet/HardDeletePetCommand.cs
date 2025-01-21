using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Volunteers.Commands.HardDeletePet;

public record HardDeletePetCommand(
    Guid VolunteerId, 
    Guid PetId) : ICommand;