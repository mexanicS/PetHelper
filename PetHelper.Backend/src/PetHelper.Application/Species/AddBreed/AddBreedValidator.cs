using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Species.AddBreed;

public class AddBreedValidator : AbstractValidator<AddBreedRequest>
{
    public AddBreedValidator()
    {
        RuleFor(request => request.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class AddBreedDtoValidator : AbstractValidator<AddBreedRequestDto>
{
    public AddBreedDtoValidator()
    {
        RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}