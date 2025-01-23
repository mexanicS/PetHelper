using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateDetailsForAssistance;

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