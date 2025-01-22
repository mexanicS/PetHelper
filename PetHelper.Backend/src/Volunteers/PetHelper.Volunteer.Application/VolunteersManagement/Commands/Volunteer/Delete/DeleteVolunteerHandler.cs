using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Delete;

public class DeleteVolunteerHandler : ICommandHandler<Guid,DeleteVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository, 
        IValidator<DeleteVolunteerCommand> validator,
        ILogger<DeleteVolunteerHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid,ErrorList>> Handle(
        DeleteVolunteerCommand command,
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
        
        await _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Volunteer with id = {volunteerId} deleted", command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}