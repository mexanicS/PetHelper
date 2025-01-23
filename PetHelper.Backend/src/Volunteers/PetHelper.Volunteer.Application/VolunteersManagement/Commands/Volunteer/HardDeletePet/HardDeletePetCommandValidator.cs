using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.HardDeletePet;


public class HardDeletePetCommandValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}