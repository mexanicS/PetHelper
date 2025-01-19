using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Queries;
using PetHelper.Application.Database;
using PetHelper.Application.DTOs.ReadDtos;
using PetHelper.Application.Extensions;
using PetHelper.Application.Models;
using PetHelper.Application.Volunteers.Queries.GetPets;

namespace PetHelper.Application.Volunteers.Queries.GetVolunteers;

public class GetVolunteersWithPaginationHandler 
    : IQueryHandler<PagedList<VolunteerDto>, GetFilteredVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    
    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PagedList<VolunteerDto>> Handle(
        GetFilteredVolunteersWithPaginationQuery query, 
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers;
        //фильтрация
        return await volunteerQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
    
}