using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Queries;

namespace PetHelper.Application.Volunteers.Queries.GetPets;

public record GetFilteredPetsWithPaginationQuery(int Page, int PageSize) : IQuery;