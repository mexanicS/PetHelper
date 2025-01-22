using PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetPets;

namespace PetHelper.Volunteer.Controllers.Requests.Pet;

public record GetPetWithPaginationRequest(int Page, int PageSize)
{
    public  GetFilteredPetsWithPaginationQuery ToQuery() => 
        new (Page, PageSize);
}