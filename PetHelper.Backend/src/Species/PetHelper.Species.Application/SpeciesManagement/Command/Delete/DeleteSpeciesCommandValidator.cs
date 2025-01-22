using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;

namespace PetHelper.Species.Application.SpeciesManagement.Command.Delete;

public class DeleteSpeciesCommandValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesCommandValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}