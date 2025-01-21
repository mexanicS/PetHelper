using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.Application.Volunteers;
using PetHelper.Domain.Models;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;
using PetHelper.Infastructure.DbContexts;

namespace PetHelper.Infastructure.Repository;

public class VolunteersRepository() : IVolunteersRepository
{
    private readonly WriteDbContext _dbcontext;

    public VolunteersRepository(WriteDbContext dbcontext) : this()
    {
         _dbcontext = dbcontext;
    }

    public async Task<Guid> AddAsync(
        Volunteer volunteer, 
        CancellationToken cancellationToken = default)
    {
        await _dbcontext.Volunteers.AddAsync(volunteer, cancellationToken);
        
        return (Guid)volunteer.Id;
    }
    
    public Task<Guid> Update(
        Volunteer volunteer, 
        CancellationToken cancellationToken = default)
    {
        _dbcontext.Volunteers.Update(volunteer);
        
        return Task.FromResult(volunteer.Id.Value);
    }
    
    public Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbcontext.Volunteers.Remove(volunteer);
        
        return Task.FromResult<Guid>(volunteer.Id);
    }

    public async Task<Result<Volunteer, Error>> GetVolunteerById(
        VolunteerId volunteerId, 
        CancellationToken cancellationToken = default)
    { 
        var volunteer = await _dbcontext.Volunteers
            .Include(v=>v.Pets)
            .FirstOrDefaultAsync(volunteer => volunteer.Id == volunteerId.Value, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(volunteerId);
        
        return volunteer;
    }
    
    public async Task<Result<IReadOnlyList<Volunteer>, Error>> GetAllVolunteers(
        VolunteerId volunteerId, 
        CancellationToken cancellationToken = default)
    { 
        var volunteers = await _dbcontext.Volunteers.ToListAsync(cancellationToken);

        return volunteers;
    }
}