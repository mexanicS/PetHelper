using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Discussion;

public record RelationId
{
    public Guid Value { get; }

    public RelationId(Guid value)
    {
        Value = value;
    }
    
    public static Result<RelationId, Error> Create(Guid value) => new RelationId(value);

    public static Result<RelationId, Error> Create() => new RelationId(Guid.NewGuid());
    
    public static implicit operator Guid(RelationId id) => id.Value;
}