namespace PetHelper.Application.Species.AddBreed;

public record AddBreedRequest(
    Guid SpeciesId,
    string Name);