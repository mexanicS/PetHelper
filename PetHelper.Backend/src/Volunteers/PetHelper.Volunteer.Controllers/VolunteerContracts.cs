using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Volunteer.Application;
using PetHelper.Volunteer.Contracts;

namespace PetHelper.Volunteer.Controllers;

public class VolunteerContracts : IVolunteerContracts
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<VolunteerContracts> _logger;

    public VolunteerContracts(
        IReadDbContext readDbContext,
        ILogger<VolunteerContracts> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }
    public async Task<bool> CheckSpeciesUsageInPets(Guid speciesId, CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(pet => pet.SpeciesId == speciesId, cancellationToken);
    }
    
    public async Task<bool> CheckBreedUsageInPets(Guid breedId, CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(pet => pet.BreedId == breedId, cancellationToken);
    }
}