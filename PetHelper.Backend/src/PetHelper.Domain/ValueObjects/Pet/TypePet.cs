using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects.Pet;

public record TypePet
{
    public const int MAX_LENGTH = 100;
    
    public string Value { get; }

    private TypePet(string value)
    {
        Value = value;
    }

    public static Result<TypePet, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            return Errors.General.ValueIsInvalid("typePet");

        return new TypePet(value);
    }
}