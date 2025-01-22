using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Species.Application.SpeciesManagement.Command.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, string BreedName) : ICommand;