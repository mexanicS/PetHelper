namespace PetHelper.Application.DTOs.ReadDtos;

public class SpeciesDto
{
    public Guid Id { get; init; }
    
    public string Name { get; init; } = string.Empty;

    public BreedDto[] Breeds { get; init; } = [];
}