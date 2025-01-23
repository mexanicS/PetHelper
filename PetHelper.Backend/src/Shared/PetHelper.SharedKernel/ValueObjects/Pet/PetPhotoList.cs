
using System.Collections;

namespace PetHelper.SharedKernel.ValueObjects.Pet;

public record PetPhotoList : IEnumerable
{
    private PetPhotoList() { }
    
    public PetPhotoList(IEnumerable<PetPhoto>? photos)
    {
        PetPhotos = (photos ?? []).ToList();
    } 
    
    public List<PetPhoto> PetPhotos { get; }
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}