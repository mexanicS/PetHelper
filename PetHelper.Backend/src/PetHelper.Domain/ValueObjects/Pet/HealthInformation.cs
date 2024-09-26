using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects;

public record HealthInformation
{
    public const int MAX_LENGTH_HEALTH_INFORMATION = 2000;
    
    public string Value { get; }

    private HealthInformation(string value)
    {
        Value = value;
    }

    public static Result<HealthInformation, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH_HEALTH_INFORMATION)
            return Errors.General.ValueIsInvalid("healthInformation");

        return new HealthInformation(value);
    }
}