using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateDetailsForAssistance;

public class UpdateDetailsForAssistanceHandler : ICommandHandler<Guid,UpdateDetailsForAssistanceCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<UpdateDetailsForAssistanceCommand> _validator;
    private readonly ILogger<UpdateDetailsForAssistanceHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDetailsForAssistanceHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<UpdateDetailsForAssistanceCommand> validator,
        ILogger<UpdateDetailsForAssistanceHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid,ErrorList>> Handle(
        UpdateDetailsForAssistanceCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteersRepository.
            GetVolunteerById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var detailsForAssistance = new DetailsForAssistanceList(
            command.UpdateDetailsForAssistanceCommandDto.DetailsForAssistanceListDto.DetailsForAssistances
                .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
        );
        
        //volunteerResult.Value.UpdateDetailsForAssistance(detailsForAssistance);
        
        await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("The list of details for assistance for user {volunteerId} has been updated", command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }

}