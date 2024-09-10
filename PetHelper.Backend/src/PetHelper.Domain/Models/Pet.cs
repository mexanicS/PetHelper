namespace PetHelper.Domain.Models
{
    public class Pet
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string TypePet { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string Breed { get; private set; } = null!;
        public string Color { get; private set; } = null!;
        public string HealthInformation { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public string PhoneNumber { get; private set; } = null!;
        public bool IsNeutered { get; private set; }
        public DateOnly? DateOfBirth { get; private set; }
        public bool IsVaccinated { get; private set; }
        public StatusPet Status { get; private set; }
        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; private set; } = [];
        public DateTime CreatedDate { get; private set; }
    }
}
