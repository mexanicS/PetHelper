using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.ValueObjects.Common;

namespace PetHelper.Application.Species.Command.Create;

public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesCommandValidator()
    {
        RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}