using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects;

public record FilePath
{
    public const int MAX_FILEPATH_LENGTH = 200;
    public string Value { get; }
    private FilePath(string path)
    {
        Value = path;
    }
    public static Result<FilePath, Error> Create(Guid path, string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return Errors.General.ValueIsInvalid(nameof(extension));
        if (path == Guid.Empty)
            return Errors.General.ValueIsInvalid(nameof(path));
        
        var fullPath = path + extension;
        return new FilePath(fullPath); 
    }
    
    public static Result<FilePath, Error> Create(string fullPath)
    {
        return new FilePath(fullPath);
    }
}