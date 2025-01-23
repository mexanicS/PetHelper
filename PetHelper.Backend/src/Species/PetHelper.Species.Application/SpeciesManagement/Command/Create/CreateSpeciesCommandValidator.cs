using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel.ValueObjects.Common;

namespace PetHelper.Species.Application.SpeciesManagement.Command.Create;

public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesCommandValidator()
    {
        RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}