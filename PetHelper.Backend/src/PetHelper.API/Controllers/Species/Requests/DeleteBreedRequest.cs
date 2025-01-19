using PetHelper.Application.Species.DeleteBreed;

namespace PetHelper.API.Controllers.Species.Requests;

public record DeleteBreedRequest(string BreedName)
{
    public DeleteBreedCommand ToCommand(Guid speciesId) =>
        new(speciesId, BreedName);
}