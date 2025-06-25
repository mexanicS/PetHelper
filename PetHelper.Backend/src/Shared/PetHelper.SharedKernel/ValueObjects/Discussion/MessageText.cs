using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Discussion;

public record MessageText
{
    public string Value { get; }

    public MessageText(string value)
    {
        Value = value;
    }

    public static Result<MessageText, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
           return Errors.General.ValueIsRequired(nameof(MessageText));
        
        return new MessageText(value);
    }
    
    public static implicit operator string(MessageText text) => text.Value;
}