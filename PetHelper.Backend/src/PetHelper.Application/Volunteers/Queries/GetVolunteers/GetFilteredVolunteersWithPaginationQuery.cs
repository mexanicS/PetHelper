using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Queries;

namespace PetHelper.Application.Volunteers.Queries.GetVolunteers;

public record GetFilteredVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;