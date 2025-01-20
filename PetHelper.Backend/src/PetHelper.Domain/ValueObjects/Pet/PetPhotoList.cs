
using System.Collections;

namespace PetHelper.Domain.ValueObjects.Pet;

public record PetPhotoList : IEnumerable
{
    private PetPhotoList() { }
    
    public PetPhotoList(IEnumerable<PetPhoto>? photos)
    {
        PetPhotos = (photos ?? []).ToList();
    } 
    
    public IReadOnlyList<PetPhoto> PetPhotos { get; }
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}