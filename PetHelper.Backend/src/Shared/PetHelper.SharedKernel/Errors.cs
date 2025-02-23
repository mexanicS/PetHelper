namespace PetHelper.SharedKernel;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }
        
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return Error.NotFound("record.not.found", $"record not found{forId}");
        }
        
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " +name+ " ";
            return Error.NotFound("record.not.found", $"invalid{label}length");
        }
        
        public static Error AlreadyExist()
        {
            return Error.Validation("record.already.exist", "Record already exist");
        }
        
        public static Error ByteCountExceeded(long byteCount)
        {
            return Error.Validation("byte.count.exceeded", $"exceeds the maximum allowed size of {byteCount} bytes");
        }
        
        public static Error Failure()
        {
            return Error.Validation("Failure", "Failure");
        }
    }

    public class Pet
    {
        public static Error PhotoNotFound(string? path = null)
        {
            return Error.Validation("photo.not.found",
                $"photo with path: {path} was not found");
        }
    }
    
    public static class User
    {
        public static Error InvalidCredentials()
        {
            return Error.Validation("credentials.is.invalid", "Your credentials is invalid");
        }
    }
    
    public static class Token
    {
        public static Error ExpiredToken()
        {
            return Error.Validation("token.is.expired", "Your token is expired");
        }

        public static Error InvalidToken()
        {
            return Error.Validation("token.is.invalid", "Your token is invalid");
        }
    }
    
    public static class Volunteer
    {
        
    }
}