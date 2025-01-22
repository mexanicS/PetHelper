using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Create;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(request => request.Email).MustBeValueObject(Email.Create);
        
        RuleFor(request => request.Description).MustBeValueObject(Description.Create);
        
        RuleFor(request => request.ExperienceInYears).MustBeValueObject(ExperienceInYears.Create);
        
        RuleFor(request => request.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        
        RuleFor(request =>
            request.FullName
        ).MustBeValueObject(x=> 
            FullName.Create(
                x.FirstName, 
                x.LastName, 
                x.MiddleName
            )
        ); 
        
        RuleForEach(request=>request.DetailsForAssistances.DetailsForAssistances)
            .MustBeValueObject(x => 
                DetailsForAssistance.Create(x.Description, x.Name));
        
        RuleForEach(request=>request.SocialNetworks.SocialNetworks)
            .MustBeValueObject(x => 
                DetailsForAssistance.Create(x.Name, x.Url));
    }
}