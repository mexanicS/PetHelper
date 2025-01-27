using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Common;

public record Name
{
    public const int MAX_LENGTH = 100;
    
    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            return Errors.General.ValueIsInvalid("name");

        return new Name(value);
    }
}