using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects;

public record Breed
{
    public const int MAX_LENGTH = 2000;
    
    public string Value { get; }

    private Breed(string value)
    {
        Value = value;
    }

    public static Result<Breed, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            return Errors.General.ValueIsInvalid("breed");

        return new Breed(value);
    }
}