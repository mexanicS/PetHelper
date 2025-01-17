using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;

namespace PetHelper.Application.Volunteers.Commands.UpdateMainInfo;

public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
    
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