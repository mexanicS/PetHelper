namespace PetHelper.Volunteer.Contracts;

public interface IVolunteerContracts
{
    Task<bool> CheckSpeciesUsageInPets(
        Guid speciesId, 
        CancellationToken cancellationToken);

    Task<bool> CheckBreedUsageInPets(
        Guid breedId,
        CancellationToken cancellationToken);
}