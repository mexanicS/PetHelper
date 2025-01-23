using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.SetMainPhotoPet;

public class SetMainPhotoPetCommandValidator : AbstractValidator<SetMainPhotoPetCommand>
{
    public SetMainPhotoPetCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.PathPhoto).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}