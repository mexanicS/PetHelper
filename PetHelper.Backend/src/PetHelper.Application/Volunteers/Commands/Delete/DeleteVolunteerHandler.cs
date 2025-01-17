using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Extensions;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.Delete;

public class DeleteVolunteerHandler : ICommandHandler<Guid,DeleteVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly ILogger<DeleteVolunteerHandler> _logger;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository, 
        IValidator<DeleteVolunteerCommand> validator,
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
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
            
        _logger.LogInformation("Volunteer with id = {volunteerId} deleted", command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}