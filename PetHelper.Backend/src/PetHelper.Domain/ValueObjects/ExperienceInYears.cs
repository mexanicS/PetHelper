namespace PetHelper.Domain.ValueObjects;

public record ExperienceInYears
{
    public int Value { get; }

    private ExperienceInYears(int value)
    {
        Value = value;
    }

    public static ExperienceInYears Create(int value) => new ExperienceInYears(value);
}