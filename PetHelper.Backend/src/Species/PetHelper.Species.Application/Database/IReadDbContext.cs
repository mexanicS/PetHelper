using PetHelper.Core.DTOs.ReadDtos;

namespace PetHelper.Species.Application.Database;

public interface IReadDbContext
{
    IQueryable<SpeciesDto> Species { get;}
    
    IQueryable<BreedDto> Breeds { get;}
}