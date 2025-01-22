using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;

namespace PetHelper.Species.Application.SpeciesManagement.Command.DeleteBreed;

public class DeleteBreedValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.BreedName).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}