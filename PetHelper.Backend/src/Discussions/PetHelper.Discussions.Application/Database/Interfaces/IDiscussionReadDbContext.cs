using PetHelper.Discussions.Application.Database.Dto;

namespace PetHelper.Discussions.Application.Database.Interfaces;

public interface IDiscussionReadDbContext
{
    IQueryable<DiscussionDto> Discussions { get; }
    
    IQueryable<MessageDto> Messages { get; }
    
    IQueryable<RelationDto> Relations { get; }
}