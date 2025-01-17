using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.Create;

public record CreateSpeciesCommand(string Name) : ICommand;