using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Application.Volunteers.Commands.SoftDeletePet;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.HardDeletePet;


public class HardDeletePetCommandValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}