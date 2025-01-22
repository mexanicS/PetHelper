using PetHelper.Core.Abstractions.Queries;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetPets;

public record GetFilteredPetsWithPaginationQuery(int Page, int PageSize) : IQuery;