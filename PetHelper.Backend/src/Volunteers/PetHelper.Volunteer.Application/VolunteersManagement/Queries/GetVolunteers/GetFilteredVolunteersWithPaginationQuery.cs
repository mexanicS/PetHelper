using PetHelper.Core.Abstractions.Queries;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetVolunteers;

public record GetFilteredVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;