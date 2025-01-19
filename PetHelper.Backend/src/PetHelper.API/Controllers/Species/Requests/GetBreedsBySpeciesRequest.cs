using PetHelper.Application.Species.Queries.GetBreedsBySpecies;

namespace PetHelper.API.Controllers.Species.Requests;

public record GetBreedsBySpeciesRequest(Guid SpeciesId)
{
    public  GetBreedsBySpeciesQuery ToQuery() => 
        new (SpeciesId);
}