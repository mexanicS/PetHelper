using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.DTOs.Pet;

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