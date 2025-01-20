using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.Command.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, string BreedName) : ICommand;