using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Queries;
using PetHelper.Application.Database;
using PetHelper.Application.DTOs.ReadDtos;
using PetHelper.Application.Extensions;
using PetHelper.Application.Models;

namespace PetHelper.Application.Volunteers.Queries.GetPets;

public class GetPetsWithPaginationHandler : IQueryHandler<PagedList<PetDto>, GetFilteredPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    
    public GetPetsWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PagedList<PetDto>> Handle(
        GetFilteredPetsWithPaginationQuery query, 
        CancellationToken cancellationToken)
    {
        var petQuery = _readDbContext.Pets;
        //фильтрация
        return await petQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}