using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.Command.Create;

public record CreateSpeciesCommand(string Name) : ICommand;