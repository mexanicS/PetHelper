using PetHelper.Species.Application.SpeciesManagement.Queries.GetSpecieses;

namespace PetHelper.Species.Controllers.Requests;

public record GetSpeciesRequest()
{
    public  GetSpeciesesQuery ToQuery() => 
        new ();
};