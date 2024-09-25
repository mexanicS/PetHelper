using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects;

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
        if (value < MAX_WEIGHT)
            return Errors.General.ValueIsInvalid("weight");

        return new Weight(value);
    }
}