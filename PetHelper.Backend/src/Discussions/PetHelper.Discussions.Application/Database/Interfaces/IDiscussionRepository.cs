using PetHelper.Discussions.Application.Database.Dto;
using PetHelper.Discussions.Domain;
using PetHelper.SharedKernel.ValueObjects.Discussion;

namespace PetHelper.Discussions.Application.Database.Interfaces;

public interface IDiscussionRepository
{
    public Task AddDiscussion(Discussion discussion);
    
    public Task<Discussion> GetDiscussionById(DiscussionId id, CancellationToken ct);
    
    public void UpdateDiscussion(Discussion discussion);
    
    public void RemoveDiscussion(Discussion discussion);
    
    public Task AddMessage(MessageDto message);
}