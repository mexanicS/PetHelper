using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
namespace PetHelper.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var volunteerResult = await _volunteersRepository.
            GetVolunteerById(VolunteerId.Create(command.Id), cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var fullNameRequest = FullName.Create(
            command.FullName.FirstName, 
            command.FullName.LastName, 
            command.FullName.MiddleName).Value;
        
        var emailRequest = Email.Create(command.Email).Value;
        
        var descriptionRequest = Description.Create(command.Description).Value;
        
        var experienceRequest = ExperienceInYears.Create(command.ExperienceInYears).Value;
        
        var phoneNumberRequest = PhoneNumber.Create(command.PhoneNumber).Value;
        
        volunteerResult.Value.UpdateMainInformation(
            fullNameRequest, 
            emailRequest, 
            descriptionRequest, 
            experienceRequest, 
            phoneNumberRequest);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
            
        _logger.LogInformation("Main information for volunteer ID {volunteerId} has been updated", command.Id);

        return volunteerResult.Value.Id.Value;
    }
}