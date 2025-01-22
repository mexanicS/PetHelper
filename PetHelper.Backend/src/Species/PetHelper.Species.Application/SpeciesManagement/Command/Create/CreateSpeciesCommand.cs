using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Species.Application.SpeciesManagement.Command.Create;

public record CreateSpeciesCommand(string Name) : ICommand;