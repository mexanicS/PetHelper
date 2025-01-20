using PetHelper.Application.Species.Queries.GetSpecieses;

namespace PetHelper.API.Controllers.Species.Requests;

public record GetSpeciesRequest()
{
    public  GetSpeciesesQuery ToQuery() => 
        new ();
};