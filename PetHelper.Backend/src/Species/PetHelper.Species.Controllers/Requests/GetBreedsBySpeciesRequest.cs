using PetHelper.Species.Application.SpeciesManagement.Queries.GetBreedsBySpecies;

namespace PetHelper.Species.Controllers.Requests;

public record GetBreedsBySpeciesRequest(Guid SpeciesId)
{
    public  GetBreedsBySpeciesQuery ToQuery() => 
        new (SpeciesId);
}