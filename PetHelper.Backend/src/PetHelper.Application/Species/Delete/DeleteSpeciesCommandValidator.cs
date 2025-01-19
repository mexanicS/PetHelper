using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Species.Delete;

public class DeleteSpeciesCommandValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesCommandValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}