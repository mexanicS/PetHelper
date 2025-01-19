namespace PetHelper.Domain.Models.Pet
{
    public class PetPhoto
    {
        public PetPhoto()
        {
            
        }
        public Guid Id { get; set; }

        public string FilePath { get; set; } = null!;

        public bool IsMain { get; set; }
    }
}
