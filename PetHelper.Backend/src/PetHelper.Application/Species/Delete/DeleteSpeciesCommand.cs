using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.Delete;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;