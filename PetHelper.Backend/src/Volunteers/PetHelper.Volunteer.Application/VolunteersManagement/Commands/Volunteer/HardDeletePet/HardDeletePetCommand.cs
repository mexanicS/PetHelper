using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.HardDeletePet;

public record HardDeletePetCommand(
    Guid VolunteerId, 
    Guid PetId) : ICommand;