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
    }

    public class Pet
    {
        public static Error PhotoNotFound(string? path = null)
        {
            return Error.Validation("photo.not.found",
                $"photo with path: {path} was not found");
        }
    }
    
    public static class Volunteer
    {
        
    }
}