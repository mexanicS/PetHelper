using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.Species.Application.Interfaces;

namespace PetHelper.Species.Application.SpeciesManagement.Command.Create;

public class CreateSpeciesHandler : ICommandHandler<Guid,CreateSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpeciesHandler(
        ISpeciesRepository speciesRepository,
        ILogger<CreateSpeciesHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid,ErrorList>> Handle(
        CreateSpeciesCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var name = Name.Create(command.Name).Value;
        var species =  await _speciesRepository.GetSpeciesByName(name, cancellationToken);

        if (species.IsSuccess)
            return Errors.General.AlreadyExist().ToErrorList();
        
        var speciesToCreate = CreateSpecies(command);
        
        await _speciesRepository.AddAsync(speciesToCreate.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Created species added with id {speciesId}", speciesToCreate.Value.Id.Value);
        
        return speciesToCreate.Value.Id.Value;
    }

    private Result<Domain.Models.Species, Error> CreateSpecies(CreateSpeciesCommand command)
    {
        var id = SpeciesId.NewId();
        var name = Name.Create(command.Name).Value;
        return new Domain.Models.Species(
            id,
            name
        );
    }
}