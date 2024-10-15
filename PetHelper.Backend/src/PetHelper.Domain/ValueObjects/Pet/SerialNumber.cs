using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects.Pet;

public record SerialNumber
{
    public int Value { get; }

    public SerialNumber(int value)
    {
        Value = value;
    }
    
    public static Result<SerialNumber, Error> Create(int number)
    {
        if(number <= 0)
            return Errors.General.ValueIsInvalid("The number must be greater than zero.");
        
        return new SerialNumber(number);
    }
}