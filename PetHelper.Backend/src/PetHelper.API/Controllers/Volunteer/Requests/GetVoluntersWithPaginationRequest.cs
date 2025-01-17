using PetHelper.Application.Volunteers.Queries.GetVolunteers;

namespace PetHelper.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest (int Page, int PageSize)
{
    public  GetFilteredVolunteersWithPaginationQuery ToQuery() => 
        new (Page, PageSize);
}