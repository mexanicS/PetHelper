using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.User;

public record UserId
{
    public Guid Value{ get; }

    public UserId(Guid value)
    {
        Value = value;
    }

    public static Result<UserId, Error> Create(Guid value)
    {
        return new UserId(value);
    }

    public static Result<UserId, Error> Create(UserId value)
    {
        return new UserId(Guid.NewGuid());
    }
    
    public static implicit operator Guid(UserId id) => id.Value;
}