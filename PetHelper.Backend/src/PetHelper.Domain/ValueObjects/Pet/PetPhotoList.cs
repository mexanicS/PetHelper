
namespace PetHelper.Domain.ValueObjects.Pet;

public record PetPhotoList
{
    private PetPhotoList() { }
    
    public PetPhotoList(IEnumerable<PetPhoto>? photos)
    {
        PetPhotos = (photos ?? []).ToList();
    } 
    
    public IReadOnlyList<PetPhoto> PetPhotos { get; }
}