using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Discussion;

public record RelationName
{
    public string Value { get; }

    public RelationName(string value)
    {
        Value = value;
    }

    public static Result<RelationName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsRequired(nameof(RelationName));
        
        return new RelationName(value);
    }
    
    public static implicit operator string(RelationName text) => text.Value;
}