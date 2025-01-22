using PetHelper.Species.Application.SpeciesManagement.Command.DeleteBreed;

namespace PetHelper.Species.Controllers.Requests;

public record DeleteBreedRequest(string BreedName)
{
    public DeleteBreedCommand ToCommand(Guid speciesId) =>
        new(speciesId, BreedName);
}