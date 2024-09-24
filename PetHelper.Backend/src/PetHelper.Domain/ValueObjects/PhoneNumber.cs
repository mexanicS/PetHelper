using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects;

public record PhoneNumber
{
    public const int PHONE_NUMBER_MAX = 15;
    public string Value { get; }
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number) && number.Length < PHONE_NUMBER_MAX)
            return Errors.General.ValueIsInvalid("title");

        return new PhoneNumber(number);
    }
}