using PetHelper.Application.Volunteers.Queries.GetPets;

namespace PetHelper.API.Controllers.Pet.Requests;

public record GetPetWithPaginationRequest(int Page, int PageSize)
{
    public  GetFilteredPetsWithPaginationQuery ToQuery() => 
        new (Page, PageSize);
}