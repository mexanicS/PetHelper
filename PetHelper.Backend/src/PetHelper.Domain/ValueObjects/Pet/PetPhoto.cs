using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects.Pet;

public record PetPhoto
{
    private PetPhoto(FilePath filePath)
    {
        FilePath = filePath;
    }
    
    public FilePath FilePath { get; }
    
    public static Result<PetPhoto, Error> Create(FilePath filePath)
    {
        return new PetPhoto(filePath);
    }
}