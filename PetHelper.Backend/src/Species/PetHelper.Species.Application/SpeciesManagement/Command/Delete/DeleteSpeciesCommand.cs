using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Species.Application.SpeciesManagement.Command.Delete;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;