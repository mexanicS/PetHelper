using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Database;
using PetHelper.Application.Extensions;
using PetHelper.Application.Species.Command.Delete;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Species.Command.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteBreedCommand> _validator;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IReadDbContext _readDbContext;

    public DeleteBreedHandler(
        ISpeciesRepository speciesRepository,
        IValidator<DeleteBreedCommand> validator,
        ILogger<DeleteSpeciesHandler> logger,
        IReadDbContext readDbContext)
    {
        _speciesRepository = speciesRepository;
        _validator = validator;
        _logger = logger;
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteBreedCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var speciesResult = await _speciesRepository
            .GetSpeciesById(SpeciesId.Create(command.SpeciesId), cancellationToken);
        
        if(speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();
        
        var foundBreed = speciesResult.Value.Breeds
            .FirstOrDefault(breed => command.BreedName.Contains(breed.Name.Value));
        
        if(foundBreed == null)
            return Error.NotFound("breed.not.found",
                "Cannot delete breed because it is not found in species").ToErrorList();
        
        if (await CheckBreedUsageInPets(foundBreed.Id.Value,cancellationToken))
            return Error.Failure("breed.use.in.pets",
                "Cannot delete breed because it is in use by pets").ToErrorList();

        speciesResult.Value.RemoveBreed(foundBreed);
        
        await _speciesRepository.Save(speciesResult.Value, cancellationToken);
        
        _logger.LogInformation($"Breed with name is {command.BreedName} deleted", command.BreedName);

        return speciesResult.Value.Id.Value;
    }
    
    private async Task<bool> CheckBreedUsageInPets(Guid breedId, CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(pet => pet.BreedId == breedId, cancellationToken);
    }
}