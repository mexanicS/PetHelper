using PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetVolunteers;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record GetVolunteersWithPaginationRequest (int Page, int PageSize)
{
    public  GetFilteredVolunteersWithPaginationQuery ToQuery() => 
        new (Page, PageSize);
}