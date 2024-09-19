namespace PetHelper.Domain.ValueObjects;

public record Email
{
    public string Value { get; }
    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value) =>
        new Email(value);
}