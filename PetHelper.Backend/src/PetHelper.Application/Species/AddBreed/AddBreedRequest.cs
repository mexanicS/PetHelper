namespace PetHelper.Application.Species.AddBreed;

public record AddBreedRequest(
    Guid SpeciesId,
    AddBreedRequestDto AddBreedRequestDto);
    
public record AddBreedRequestDto(
    string Name);