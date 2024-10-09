using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.Application.Volunteers;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

namespace PetHelper.Infastructure.Repository;

public class VolunteersRepository() : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbcontext;

    public VolunteersRepository(ApplicationDbContext dbcontext) : this()
    {
         _dbcontext = dbcontext;
    }

    public async Task<Guid> Add(
        Volunteer volunteer, 
        CancellationToken cancellationToken = default)
    {
        await _dbcontext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbcontext.SaveChangesAsync(cancellationToken);
        
        return (Guid)volunteer.Id;
    }
    
    public async Task<Guid> Save(
        Volunteer volunteer, 
        CancellationToken cancellationToken = default)
    {
        _dbcontext.Volunteers.Attach(volunteer);
        await _dbcontext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
    }

    public async Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbcontext.Volunteers.Remove(volunteer);
        await _dbcontext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
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
}