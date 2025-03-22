using CSharpFunctionalExtensions;
using PetHelper.Core.Abstractions.Queries;
using PetHelper.Core.DTOs.ReadDtos;
using PetHelper.Core.Extensions;
using PetHelper.Core.Models;
using PetHelper.SharedKernel;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetVolunteers;

public class GetVolunteersWithPaginationHandler 
    : IQueryHandler<PagedList<VolunteerDto>, GetFilteredVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    
    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<Result<PagedList<VolunteerDto>, ErrorList>> Handle(
        GetFilteredVolunteersWithPaginationQuery query, 
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers;
        //фильтрация
        return await volunteerQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}