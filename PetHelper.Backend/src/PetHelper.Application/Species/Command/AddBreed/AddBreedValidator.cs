using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects.Common;

namespace PetHelper.Application.Species.Command.AddBreed;

public class AddBreedValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedValidator()
    {
        RuleFor(request => request.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class AddBreedDtoValidator : AbstractValidator<AddBreedCommandDto>
{
    public AddBreedDtoValidator()
    {
        RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}