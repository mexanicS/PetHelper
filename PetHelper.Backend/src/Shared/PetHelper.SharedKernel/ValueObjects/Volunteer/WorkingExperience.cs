using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Volunteer;

public record WorkingExperience
{
    private WorkingExperience(int value)
    {
        Value = value;
    }
    public int Value { get; } = default!;
    
    public static Result<WorkingExperience, Error> Create(int value)
    {
        if (value < 0)
            return Errors.General.ValueIsInvalid("working experience value must be greater than zero");
        
        var newWorkingExperience = new WorkingExperience(value);

        return newWorkingExperience;
    }
}