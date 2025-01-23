using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.Species.Application.Database;
using PetHelper.Species.Application.Interfaces;
using PetHelper.Species.Application.SpeciesManagement.Command.Delete;
using PetHelper.Volunteer.Contracts;

namespace PetHelper.Species.Application.SpeciesManagement.Command.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteBreedCommand> _validator;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerContracts _volunteerContracts;

    public DeleteBreedHandler(
        ISpeciesRepository speciesRepository,
        IValidator<DeleteBreedCommand> validator,
        ILogger<DeleteSpeciesHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork,
        IVolunteerContracts volunteerContracts)
    {
        _speciesRepository = speciesRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteerContracts = volunteerContracts;
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
        
        if (await  _volunteerContracts.CheckBreedUsageInPets(foundBreed.Id.Value,cancellationToken))
            return Error.Failure("breed.use.in.pets",
                "Cannot delete breed because it is in use by pets").ToErrorList();

        speciesResult.Value.RemoveBreed(foundBreed);
        
        await _speciesRepository.Update(speciesResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"Breed with name is {command.BreedName} deleted", command.BreedName);

        return speciesResult.Value.Id.Value;
    }
}