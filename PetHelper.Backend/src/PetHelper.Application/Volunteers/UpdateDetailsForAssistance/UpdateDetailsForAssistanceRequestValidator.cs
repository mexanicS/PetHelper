using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.UpdateDetailsForAssistance;

public class UpdateDetailsForAssistanceRequestValidator : AbstractValidator<UpdateDetailsForAssistanceRequest>
{
    public UpdateDetailsForAssistanceRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateDetailsForAssistanceRequestDtoValidator : AbstractValidator<UpdateDetailsForAssistanceRequestDto>
{
    public UpdateDetailsForAssistanceRequestDtoValidator()
    {
        RuleForEach(request=>request.DetailsForAssistanceListDto.DetailsForAssistances)
            .MustBeValueObject(x => 
                DetailsForAssistance.Create(x.Name, x.Description));
    }
}