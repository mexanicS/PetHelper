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

    public async Task<Result<Guid>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var id = VolunteerId.NewId();
        
        var fullNameDto = request.FullName;
        var emailDto = request.Email;
        var descriptionDto = request.Description;
        var experienceDto = request.ExperienceInYears;
        var phoneNumberDto = request.PhoneNumber;
        var volunteerDetailsDto = request.VolunteerDetails;
        
        var fullName = FullName.Create(fullNameDto.FirstName, fullNameDto.LastName, fullNameDto.MiddleName);
        var email = Email.Create(emailDto);
        var description = Description.Create(descriptionDto);
        var experience = ExperienceInYears.Create(experienceDto);
        var phoneNumber = PhoneNumber.Create(phoneNumberDto);

        var volunteerDetailsList = VolunteerDetails.Create(
            SocialNetworkList.Create(
                volunteerDetailsDto.SocialNetworks.SocialNetworks
                    .Select(c => SocialNetwork.Create(c.Name, c.Url))
            ).SocialNetworks, 
            DetailsForAssistanceList.Create(
                volunteerDetailsDto.DetailsForAssistances.DetailsForAssistance
                    .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
            ).DetailsForAssistances);
        
        
        var volunteer = new Volunteer(
            id,
            fullName,
            email,
            description,
            experience,
            phoneNumber,
            volunteerDetailsList
        );
        
        await _volunteersRepository.Add(volunteer, cancellationToken);
        
        return volunteer.Id.Value;
    }
}