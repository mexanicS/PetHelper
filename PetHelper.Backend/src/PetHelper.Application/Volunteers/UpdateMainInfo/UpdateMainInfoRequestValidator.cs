using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateMainInfoHandlerDtoValidator : AbstractValidator<UpdateMainInfoDto>
{
    public UpdateMainInfoHandlerDtoValidator()
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
    }
}