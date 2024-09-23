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
        var id = CreateVolunteerId();
        var fullName = CreateFullName(request.FullName);
        var email = CreateEmail(request.Email);
        var description = CreateDescription(request.Description);
        var experience = CreateExperience(request.ExperienceInYears);
        var phoneNumber = CreatePhoneNumber(request.PhoneNumber);
        var socialNetwork = CreateSocialNetworkList(request.SocialNetworks);
        var detailsForAssistance = CreateDetailsForAssistanceList(request.DetailsForAssistances);

        var volunteer = CreateVolunteer(
            id, 
            fullName, 
            email, 
            description, 
            experience, 
            phoneNumber, 
            socialNetwork, 
            detailsForAssistance
        );

        await AddVolunteerToRepository(volunteer, cancellationToken);

        return volunteer.Id.Value;
    }

    private VolunteerId CreateVolunteerId()
    {
        return VolunteerId.NewId();
    }

    private FullName CreateFullName(FullNameDto fullNameDto)
    {
        return FullName.Create(fullNameDto.FirstName, fullNameDto.LastName, fullNameDto.MiddleName);
    }

    private Email CreateEmail(string emailDto)
    {
        return Email.Create(emailDto);
    }

    private Description CreateDescription(string descriptionDto)
    {
        return Description.Create(descriptionDto);
    }

    private ExperienceInYears CreateExperience(int experienceDto)
    {
        return ExperienceInYears.Create(experienceDto);
    }

    private PhoneNumber CreatePhoneNumber(string phoneNumberDto)
    {
        return PhoneNumber.Create(phoneNumberDto);
    }

    private SocialNetworkList CreateSocialNetworkList(SocialNetworkListDto socialNetworkDto)
    {
        return new SocialNetworkList(
            socialNetworkDto.SocialNetworks
                .Select(c => SocialNetwork.Create(c.Name, c.Url))
        );
    }

    private DetailsForAssistanceList CreateDetailsForAssistanceList(DetailsForAssistanceListDto detailsForAssistanceDto)
    {
        return new DetailsForAssistanceList(
            detailsForAssistanceDto.DetailsForAssistances
                .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
        );
    }

    private Volunteer CreateVolunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        ExperienceInYears experience,
        PhoneNumber phoneNumber,
        SocialNetworkList socialNetwork,
        DetailsForAssistanceList detailsForAssistance
    )
    {
        return new Volunteer(
            id,
            fullName,
            email,
            description,
            experience,
            phoneNumber,
            socialNetwork,
            detailsForAssistance
        );
    }

    private async Task AddVolunteerToRepository(Volunteer volunteer, CancellationToken cancellationToken)
    {
        await _volunteersRepository.Add(volunteer, cancellationToken);
    }
}