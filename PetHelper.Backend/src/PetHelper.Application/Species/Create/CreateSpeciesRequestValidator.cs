using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Species.Create;

public class CreateSpeciesRequestValidator : AbstractValidator<CreateSpeciesRequest>
{
    public CreateSpeciesRequestValidator()
    {
        RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}