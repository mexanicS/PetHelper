using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects.Pet;

public record PetPhoto
{
    private PetPhoto(FilePath filePath, bool isMain)
    {
        FilePath = filePath;
        IsMain = isMain;
    }
    
    public bool IsMain { get; }
    public FilePath FilePath { get; }
    
    public static Result<PetPhoto, ErrorList> Create(FilePath filePath, bool isMain = false)
    {
        return new PetPhoto(filePath, isMain);
    }
}