using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects;

public record ExperienceInYears
{
    public const int MAX_EXPERIENCE_IN_YEARS = 120;
    public int Value { get; }

    private ExperienceInYears(int value)
    {
        Value = value;
    }

    public static Result<ExperienceInYears, Error> Create(int value)
    {
        if (value is < 0 or > MAX_EXPERIENCE_IN_YEARS)
            return Errors.General.ValueIsInvalid("experienceInYears");

        return new ExperienceInYears(value);
    }
}