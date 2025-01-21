using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Application.Volunteers.Commands.UpdateDetailsForAssistance;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.SetMainPhotoPet;

public class SetMainPhotoPetCommandValidator : AbstractValidator<SetMainPhotoPetCommand>
{
    public SetMainPhotoPetCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.PathPhoto).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}