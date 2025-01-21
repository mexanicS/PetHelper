using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Database;
using PetHelper.Application.Extensions;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Species.Command.Delete;

public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<DeleteSpeciesCommand> _validator;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSpeciesHandler(
        ISpeciesRepository speciesRepository,
        IValidator<DeleteSpeciesCommand> validator,
        ILogger<DeleteSpeciesHandler> logger,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _validator = validator;
        _logger = logger;
        _readDbContext = readDbContext;
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
        
        if (await CheckSpeciesUsageInPets(speciesResult.Value.Id.Value,cancellationToken))
            return Error.Failure("species.use.in.pets",
                "Cannot delete species because it is in use by pets").ToErrorList();
        
        await _speciesRepository.Delete(speciesResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"Species with id = {command.SpeciesId} deleted", command.SpeciesId);

        return speciesResult.Value.Id.Value;
    }
    
    private async Task<bool> CheckSpeciesUsageInPets(Guid speciesId, CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(pet => pet.SpeciesId == speciesId, cancellationToken);
    }
}
