using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects.Pet;

public record Height
{
    public const int MAX_HEIGHT = 1000;
    
    public double Value { get; }

    private Height(double value)
    {
        Value = value;
    }

    public static Result<Height, Error> Create(double value)
    {
        if (value is > 0 and > MAX_HEIGHT)
            return Errors.General.ValueIsInvalid("height");

        return new Height(value);
    }
}