using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.SetMainPhotoPet;

public record SetMainPhotoPetCommand(
    Guid VolunteerId, 
    Guid PetId, 
    string PathPhoto) : ICommand;