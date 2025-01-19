using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;

namespace PetHelper.Application.Species.Create;

public class CreateSpeciesRequestValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesRequestValidator()
    {
        RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}