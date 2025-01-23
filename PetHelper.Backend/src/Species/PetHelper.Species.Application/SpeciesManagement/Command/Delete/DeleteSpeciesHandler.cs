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
using PetHelper.Volunteer.Contracts;

namespace PetHelper.Species.Application.SpeciesManagement.Command.Delete;

public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteSpeciesCommand> _validator;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IVolunteerContracts _volunteerContracts;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSpeciesHandler(
        ISpeciesRepository speciesRepository,
        IValidator<DeleteSpeciesCommand> validator,
        ILogger<DeleteSpeciesHandler> logger,
        IVolunteerContracts volunteerContracts,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _validator = validator;
        _logger = logger;
        _volunteerContracts = volunteerContracts;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteSpeciesCommand command, 
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
        
        if (await _volunteerContracts.CheckSpeciesUsageInPets(speciesResult.Value.Id.Value,cancellationToken))
            return Error.Failure("species.use.in.pets",
                "Cannot delete species because it is in use by pets").ToErrorList();
        
        await _speciesRepository.Delete(speciesResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"Species with id = {command.SpeciesId} deleted", command.SpeciesId);

        return speciesResult.Value.Id.Value;
    }
    
    
}
