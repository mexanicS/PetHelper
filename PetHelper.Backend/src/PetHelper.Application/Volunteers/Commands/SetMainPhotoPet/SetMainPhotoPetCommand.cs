using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Volunteers.Commands.SetMainPhotoPet;

public record SetMainPhotoPetCommand(
    Guid VolunteerId, 
    Guid PetId, 
    string PathPhoto) : ICommand;