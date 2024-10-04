using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Species.AddBreed;

public class AddBreedValidator : AbstractValidator<AddBreedRequest>
{
    public AddBreedValidator()
    {
        RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}