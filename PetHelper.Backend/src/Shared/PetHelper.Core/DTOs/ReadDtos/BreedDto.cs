namespace PetHelper.Core.DTOs.ReadDtos;

public class BreedDto
{
    public Guid Id { get; set; }
    
    public Guid SpeciesId { get; set; }
    
    public string Name { get; }
}