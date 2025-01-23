using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects.Pet;

public record PetPhoto
{
    public PetPhoto()
    {
        
    }
    private PetPhoto(string filePath, bool isMain)
    {
        FilePath = filePath;
        IsMain = isMain;
    }
    
    public bool IsMain { get; set; }
    public string FilePath { get; set; }
    
    public static Result<PetPhoto, ErrorList> Create(string filePath, bool isMain = false)
    {
        return new PetPhoto(filePath, isMain);
    }
}