using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Species.Create;

public class CreateSpeciesHandler
{
    private readonly ISpeciesRepository _speciesRepository;
    
    private readonly ILogger _logger;
    public CreateSpeciesHandler(
        ISpeciesRepository speciesRepository,
        ILogger<CreateSpeciesHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        CreateSpeciesRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = Name.Create(request.Name).Value;
        var species =  await _speciesRepository.GetSpeciesByName(name, cancellationToken);

        if (species.IsSuccess)
            return Errors.General.AlreadyExist();
        
        var speciesToCreate = CreateSpecies(request);
        
        await _speciesRepository.Add(speciesToCreate.Value, cancellationToken);
        _logger.LogInformation("Created species added with id {speciesId}", speciesToCreate.Value.Id.Value);
        
        return speciesToCreate.Value.Id.Value;
    }

    private Result<Domain.Models.Species.Species, Error> CreateSpecies(CreateSpeciesRequest request)
    {
        var id = SpeciesId.NewId();
        var name = Name.Create(request.Name).Value;
        return new Domain.Models.Species.Species(
            id,
            name
        );
    }
}