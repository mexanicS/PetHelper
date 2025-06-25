using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Common;

public record Date
{
    private const int MAX_YEAR = 3000;
    private const int MIN_YEAR = 3000;

    private Date() { }

    public DateTime Value { get; init; }
    public Date(DateTime value)
    {
        Value = value;
    }

    public static Result<Date, Error> Create(DateTime value)
    {
        if (value.Year is > MAX_YEAR or < MIN_YEAR)
        {
            return Errors.Validation("Дата");
        }
        
        return new Date(value.ToUniversalTime());
    }
    
    public static Result<Date, Error> Create()
    {
        return new Date(DateTime.UtcNow);
    }
}