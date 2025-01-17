using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.Commands.UpdateDetailsForAssistance;

public class UpdateDetailsForAssistanceCommandValidator : AbstractValidator<UpdateDetailsForAssistanceCommand>
{
    public UpdateDetailsForAssistanceCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateDetailsForAssistanceRequestDtoValidator : AbstractValidator<UpdateDetailsForAssistanceCommandDto>
{
    public UpdateDetailsForAssistanceRequestDtoValidator()
    {
        RuleForEach(request=>request.DetailsForAssistanceListDto.DetailsForAssistances)
            .MustBeValueObject(x => 
                DetailsForAssistance.Create(x.Name, x.Description));
    }
}