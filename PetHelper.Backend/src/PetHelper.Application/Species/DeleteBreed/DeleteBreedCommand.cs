using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, string BreedName) : ICommand;