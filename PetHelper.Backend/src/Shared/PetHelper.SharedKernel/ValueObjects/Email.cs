using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects;

public record Email
{
    public const int MAX_LENGTH = 100;
    
    public string Value { get; }
    
    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || MAX_LENGTH < value.Length)
            return Errors.General.ValueIsInvalid("email");

        return new Email(value);
    }
}