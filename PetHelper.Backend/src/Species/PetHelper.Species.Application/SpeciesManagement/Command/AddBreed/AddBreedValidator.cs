using FluentValidation;

namespace PetHelper.Species.Application.SpeciesManagement.Command.AddBreed;

public class AddBreedValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedValidator()
    {
        RuleFor(request => request.SpeciesId).NotEmpty().NotEmpty();
    }
}

public class AddBreedDtoValidator : AbstractValidator<AddBreedCommandDto>
{
    public AddBreedDtoValidator()
    {
        //RuleFor(request => request.Name).MustBeValueObject(Name.Create);
    }
}