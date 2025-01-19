using FluentValidation;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.AddPetPhotos;

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