using FluentValidation;
using PetHelper.Core;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPetPhotos;

public class AddPetPhotosValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotosValidator()
    {
        RuleFor(x => x.VolunteerId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid())
            .NotEqual(Guid.Empty).WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.PetId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid())
            .NotEqual(Guid.Empty).WithError(Errors.General.ValueIsInvalid());
        
        RuleForEach(x => x.Files).SetValidator(new UploadFileDtoValidator());
    }
}