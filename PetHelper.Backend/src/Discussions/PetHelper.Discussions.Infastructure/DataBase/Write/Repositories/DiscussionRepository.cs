using Microsoft.EntityFrameworkCore;
using PetHelper.Discussions.Application.Database.Dto;
using PetHelper.Discussions.Application.Database.Interfaces;
using PetHelper.Discussions.Domain;
using PetHelper.SharedKernel.ValueObjects.Discussion;

namespace PetHelper.Discussions.Infastructure.DataBase.Write.Repositories;

public class DiscussionRepository(DiscussionDbContext dbContext) : IDiscussionRepository
{
    public async Task AddDiscussion(Discussion discussion)
    {
        await dbContext.Discussions.AddAsync(discussion);
    }

    public async Task<Discussion?> GetDiscussionById(Guid id, CancellationToken ct)
    {
        var discission = await dbContext.Discussions
            .Include(d=>d.Relation)
            .FirstOrDefaultAsync(d=>d.Id == id, ct);
        
        return discission;
    }

    public async void UpdateDiscussion(Discussion discussion)
    {
        dbContext.Discussions.Update(discussion);
    }

    public async void RemoveDiscussion(Discussion discussion)
    {
        dbContext.Discussions.Remove(discussion);
    }
}