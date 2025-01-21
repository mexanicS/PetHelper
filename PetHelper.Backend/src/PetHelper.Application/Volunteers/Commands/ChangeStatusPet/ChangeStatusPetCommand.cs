using PetHelper.Application.Abstractions.Commands;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.ChangeStatusPet;

public record ChangeStatusPetCommand(
    Guid VolunteerId,
    Guid PetId,
    string Status) : ICommand;