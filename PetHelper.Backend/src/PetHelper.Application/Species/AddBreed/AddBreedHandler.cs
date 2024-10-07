using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using Breed = PetHelper.Domain.Models.Breed.Breed;

namespace PetHelper.Application.Species.AddBreed;

public class AddBreedHandler
{
    private readonly ISpeciesRepository _speciesRepository;
    
    private readonly ILogger _logger;
    public AddBreedHandler(
        ISpeciesRepository speciesRepository,
        ILogger<AddBreedHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        AddBreedRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var breedName = Name.Create(request.AddBreedRequestDto.Name).Value;
        
        var speciesId = SpeciesId.Create(request.SpeciesId);
        var species =  await _speciesRepository.GetSpeciesById(speciesId, cancellationToken);
        
        if (species.IsFailure)
            return Errors.General.NotFound();

        if (species.Value.Breeds.Any(breed => breed.Name == breedName))
        {
            return Errors.General.AlreadyExist();
        }
        
        var breedToCreate = CreateBreed(request);

        if (breedToCreate.IsFailure)
            return breedToCreate.Error;
        
        species.Value.AddBreed(breedToCreate.Value);
        await _speciesRepository.Save(species.Value, cancellationToken);
        
        _logger.LogInformation("Added breed with species id {speciesId}", breedToCreate.Value.Id.Value);
        
        return breedToCreate.Value.Id.Value;
    }
    
    private Result<Breed, Error> CreateBreed(AddBreedRequest request)
    {
        var id = BreedId.NewId();
        
        var name = Name.Create(request.AddBreedRequestDto.Name).Value;
        return new Breed(
            id,
            name
        );
    }
}