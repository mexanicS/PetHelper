using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.DTOs;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.CreateVolunteers;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    
    private readonly ILogger _logger;
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid,Error>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var volunteer = CreateVolunteer(request);
        if (volunteer.IsFailure)
            return volunteer.Error;
        
        await _volunteersRepository.Add(volunteer.Value, cancellationToken);
        _logger.LogInformation("Created volunteer added with id {volunteerId}", volunteer.Value.Id.Value);
        
        return volunteer.Value.Id.Value;
    }
    private Result<Volunteer, Error> CreateVolunteer(CreateVolunteerRequest request)
    {
        var id = VolunteerId.NewId();
        var fullNameRequest = FullName.Create(
            request.FullName.FirstName, 
            request.FullName.LastName, 
            request.FullName.MiddleName).Value;
        
        var email = Email.Create(request.Email).Value;
        
        var description = Description.Create(request.Description).Value;
        
        var experience = ExperienceInYears.Create(request.ExperienceInYears).Value;
        
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;
        
        var socialNetwork = new SocialNetworkList(
            request.SocialNetworks.SocialNetworks
                .Select(c => SocialNetwork.Create(c.Name, c.Url).Value)
        );
        
        var detailsForAssistance = new DetailsForAssistanceList(
            request.DetailsForAssistances.DetailsForAssistances
                .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
        );

        return new Volunteer(
            id,
            fullNameRequest,
            email,
            description,
            experience,
            phoneNumber,
            socialNetwork,
            detailsForAssistance
        );
    }
}