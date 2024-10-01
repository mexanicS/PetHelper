using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Volunteers.CreateVolunteers;
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
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var volunteerResult = await _volunteersRepository.
            GetVolunteerById(VolunteerId.Create(request.VolunteerId), cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var fullNameRequest = FullName.Create(
            request.Dto.FullName.FirstName, 
            request.Dto.FullName.LastName, 
            request.Dto.FullName.MiddleName).Value;
        
        var emailRequest = Email.Create(request.Dto.Email).Value;
        
        var descriptionRequest = Description.Create(request.Dto.Description).Value;
        
        var experienceRequest = ExperienceInYears.Create(request.Dto.ExperienceInYears).Value;
        
        var phoneNumberRequest = PhoneNumber.Create(request.Dto.PhoneNumber).Value;
        
        volunteerResult.Value.UpdateMainInformation(
            fullNameRequest, 
            emailRequest, 
            descriptionRequest, 
            experienceRequest, 
            phoneNumberRequest);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
            
        _logger.LogInformation("Main information for volunteer ID {volunteerId} has been updated", request.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}