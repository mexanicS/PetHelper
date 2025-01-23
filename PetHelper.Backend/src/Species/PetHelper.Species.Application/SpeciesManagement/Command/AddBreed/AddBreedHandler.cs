using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.Species.Application.Interfaces;
using PetHelper.Species.Domain.Models;

namespace PetHelper.Species.Application.SpeciesManagement.Command.AddBreed;

public class AddBreedHandler : ICommandHandler<Guid,AddBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<AddBreedCommand> _validator;

    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddBreedHandler(
        ISpeciesRepository speciesRepository,
        IValidator<AddBreedCommand> validator,
        ILogger<AddBreedHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid,ErrorList>> Handle(
        AddBreedCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var breedName = Name.Create(command.AddBreedCommandDto.Name).Value;
        
        var speciesId = SpeciesId.Create(command.SpeciesId);
        var species =  await _speciesRepository.GetSpeciesById(speciesId, cancellationToken);
        
        if (species.IsFailure)
            return Errors.General.NotFound().ToErrorList();

        if (species.Value.Breeds.Any(breed => breed.Name == breedName))
        {
            return Errors.General.AlreadyExist().ToErrorList();
        }
        
        var breedToCreate = CreateBreed(command);

        if (breedToCreate.IsFailure)
            return breedToCreate.Error.ToErrorList();
        
        species.Value.AddBreed(breedToCreate.Value);
        
        await _speciesRepository.Update(species.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Added breed with species id {speciesId}", breedToCreate.Value.Id.Value);
        
        return breedToCreate.Value.Id.Value;
    }
    
    private Result<Breed, Error> CreateBreed(AddBreedCommand command)
    {
        var id = BreedId.NewId();
        
        var name = Name.Create(command.AddBreedCommandDto.Name).Value;
        return new Breed(
            id,
            name
        );
    }
}