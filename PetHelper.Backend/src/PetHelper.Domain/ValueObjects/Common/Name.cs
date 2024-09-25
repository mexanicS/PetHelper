using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects;

public record Name
{
    public const int MAX_LENGTH_NAME = 100;
    
    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH_NAME)
            return Errors.General.ValueIsInvalid("name");

        return new Name(value);
    }
}