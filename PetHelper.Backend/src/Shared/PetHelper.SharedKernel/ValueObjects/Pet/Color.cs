using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Pet;

public record Color
{
    public const int MAX_LENGTH_COLOR = 2000;
    
    public string Value { get; }

    private Color(string value)
    {
        Value = value;
    }

    public static Result<Color, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH_COLOR)
            return Errors.General.ValueIsInvalid("color");

        return new Color(value);
    }
}