using CSharpFunctionalExtensions;
using PetHelper.Application.DTOs;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.CreateVolunteers;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
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
        
        return volunteer.Value.Id.Value;
    }
    private Result<Volunteer, Error> CreateVolunteer(CreateVolunteerRequest request)
    {
        var id = VolunteerId.NewId();
        var fullNameRequest = FullName.Create(
            request.FullName.FirstName, 
            request.FullName.LastName, 
            request.FullName.MiddleName);

        if (fullNameRequest.IsFailure)
            return fullNameRequest.Error;
            
        var email = Email.Create(request.Email);
        if (email.IsFailure)
            return email.Error;
        
        var description = Description.Create(request.Description);
        if (description.IsFailure)
            return description.Error;
        
        var experience = ExperienceInYears.Create(request.ExperienceInYears);
        if (experience.IsFailure)
            return experience.Error;
        
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
            return phoneNumber.Error;
        
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
            fullNameRequest.Value,
            email.Value,
            description.Value,
            experience.Value,
            phoneNumber.Value,
            socialNetwork,
            detailsForAssistance
        );
    }
}