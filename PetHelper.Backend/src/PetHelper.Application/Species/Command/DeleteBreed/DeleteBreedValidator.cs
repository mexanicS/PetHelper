using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Species.Command.DeleteBreed;

public class DeleteBreedValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.BreedName).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}