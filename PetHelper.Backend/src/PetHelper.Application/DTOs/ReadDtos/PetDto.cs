using PetHelper.Domain.Shared;

namespace PetHelper.Application.DTOs.ReadDtos;

public class PetDto
{
    public Guid Id { get; init; }
    
    public Guid VolunteerId { get; init; }
    
    public string Name { get; init; } = null!;

    public string TypePet { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Color { get; init; } = null!;

    public string HealthInformation { get; init; } = null!;
    
    public string City { get; private set; } = null!;
    
    public string Street { get; private set; } = null!;
    
    public string HouseNumber { get; private set; } = null!;
    
    public string? ZipCode { get; private set; }

    public double Weight { get; private set; }

    public double Height { get; private set; }

    public string PhoneNumber { get; private set; } = null!;

    public bool IsNeutered { get; private set; }

    public DateOnly? DateOfBirth { get; private set; }

    public bool IsVaccinated { get; private set; }

    public Constants.StatusPet Status { get; private set; }

    public DateTime CreatedDate { get; private set; }
        
    //public Guid SpeciesBreed { get; private set; }
        
    public int SerialNumber { get; private set; }
        
    public int Position { get; private set; } 
    
    //public  PetPhotosList { get; private set; }= null!;
    
    //public PetDetails PetDetails { get; private set; }
}