using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Pet;

public record Weight
{
    public const int MAX_WEIGHT = 1000;
    
    public double Value { get; }

    private Weight(double value)
    {
        Value = value;
    }

    public static Result<Weight, Error> Create(double value)
    {
        if (value is > 0 and > MAX_WEIGHT)
            return Errors.General.ValueIsInvalid("weight");

        return new Weight(value);
    }
}