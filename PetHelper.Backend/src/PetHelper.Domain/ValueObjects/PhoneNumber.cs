using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects;

public record PhoneNumber
{
    public const int MAX_LENGTH = 15;
    public string Value { get; }
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Length > MAX_LENGTH)
            return Errors.General.ValueIsInvalid("phoneNumber");

        return new PhoneNumber(number);
    }
}