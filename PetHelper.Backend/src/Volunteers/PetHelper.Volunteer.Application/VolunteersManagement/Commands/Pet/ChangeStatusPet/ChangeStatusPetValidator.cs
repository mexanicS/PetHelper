using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.ChangeStatusPet;

public class ChangeStatusPetValidator : AbstractValidator<ChangeStatusPetCommand>
{
    public ChangeStatusPetValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleFor(request => request.Status)
            .Must(BeValidStatus)
            .WithError(Error.NotFound("status.not.changed","Status can only be changed to NeedsHelp or LookingForHome"));
    }
    
    private bool BeValidStatus(string status)
    {
        if (Enum.TryParse<Constants.StatusPet>(status, true, out var parsedStatus))
        {
            return parsedStatus is Constants.StatusPet.NeedsHelp or Constants.StatusPet.LookingForHome;
        }

        return false;
    }
}