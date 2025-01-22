using FluentValidation;
using PetHelper.Core.DTOs.Pet;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;

namespace PetHelper.Core;

public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(x => x.FileName).NotEmpty()
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.Content.Length).LessThan(5000000)
            .WithError(Errors.General.ByteCountExceeded(5000000));
    }
}