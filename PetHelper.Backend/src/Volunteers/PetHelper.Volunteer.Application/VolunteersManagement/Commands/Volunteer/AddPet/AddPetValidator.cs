using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPet;

public class AddPetValidator : AbstractValidator<AddPetCommand>
{
    public AddPetValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(Name.MAX_LENGTH)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.Description).NotEmpty()
            .MaximumLength(Description.MAX_LENGTH)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.Color).NotEmpty()
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.Weight).GreaterThan(0)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.Height).GreaterThan(0)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.SpeciesId).NotEqual(Guid.Empty)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.BreedId).NotEqual(Guid.Empty)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.HealthInformation)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.City).NotEmpty()
            .MaximumLength(Address.MAX_LENGTH)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.Street).NotEmpty()
            .MaximumLength(Address.MAX_LENGTH)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.HouseNumber).NotEmpty()
            .MaximumLength(Address.MAX_LENGTH)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.ZipCode)
            .MaximumLength(Address.MAX_LENGTH)
            .WithError(Errors.General.ValueIsInvalid());
        
        RuleFor(x => x.PhoneNumber).NotEmpty()
            .MaximumLength(PhoneNumber.MAX_LENGTH)
            .WithError(Errors.General.ValueIsInvalid());
        
    }
}