using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.Command.Delete;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;