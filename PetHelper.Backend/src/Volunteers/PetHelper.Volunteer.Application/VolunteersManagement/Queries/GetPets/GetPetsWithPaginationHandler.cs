using CSharpFunctionalExtensions;
using PetHelper.Core.Abstractions.Queries;
using PetHelper.Core.DTOs.ReadDtos;
using PetHelper.Core.Extensions;
using PetHelper.Core.Models;
using PetHelper.SharedKernel;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetPets;

public class GetPetsWithPaginationHandler 
    : IQueryHandler<PagedList<PetDto>, GetFilteredPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    
    public GetPetsWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<PagedList<PetDto>, ErrorList>> Handle(
        GetFilteredPetsWithPaginationQuery query, 
        CancellationToken cancellationToken)
    {
        var petQuery = _readDbContext.Pets;
        //фильтрация
        return await petQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}