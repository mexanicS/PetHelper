using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Discussion;
using PetHelper.SharedKernel.ValueObjects.User;

namespace PetHelper.Discussions.Domain;

public class Discussion
{
    public DiscussionId Id { get; private set; }
    
    public RelationId RelationId { get; private set; }
    
    public Relation Relation { get; private set; }
    
    public List<UserId> UserIds { get; private set; }
    
    public List<Message> Messages { get; private set; }
    
    public Constants.DiscussionStatus Status { get; private set; }

    private Discussion(RelationId relationId, IEnumerable<UserId> userIds)
    {
        Id = DiscussionId.Create(relationId).Value;
        RelationId = relationId;
        UserIds = userIds.ToList();
        Status = Constants.DiscussionStatus.Open;
    }
    
    public static Result<Discussion, Error> Create(
        RelationId relationId, IEnumerable<UserId> userIds)
    {
        if (userIds.Count() < 2)
            return Errors.Discussion.UsersCountError();

        return new Discussion(relationId, userIds);
    }

    public UnitResult<Error> AddMessage(Message message)
    {
        /*if (Status == Constants.DiscussionStatus.Close)
            return Errors.Discussion.DiscussionIsClosed();
         
        var isUserParticipatesInDiscussion = UserIds.Contains(message.UserId);

        if (!isUserParticipatesInDiscussion)
            return Errors.Discussion.IsNotParticipantError();*/
        
        Messages.Add(message);
        return Result.Success<Error>();
    }
    
    public void Close() => Status = Constants.DiscussionStatus.Close;
    
    public void Open() => Status = Constants.DiscussionStatus.Open;
    
    public UnitResult<Error> EditMessage(Message message, MessageText newMessageText)
    {
        /*if (Status == Constants.DiscussionStatus.Close)
            return Errors.Discussion.DiscussionIsClosed();
        
        var isUserParticipatesInDiscussion = UserIds.Contains(message.UserId);

        if (!isUserParticipatesInDiscussion)
            return Errors.Discussion.IsNotParticipantError();
        
        var messageToUpdate = Messages.FirstOrDefault(m => m.Id == message.Id);
        if (messageToUpdate is null)
            return Errors.Message.MessageNotFound();*/

        message = message.Edit(newMessageText);

        return Result.Success<Error>();
    }
    
    public UnitResult<Error> AddUser(UserId userId)
    {
        /*if (Status == Constants.DiscussionStatus.Close)
            return Errors.Discussion.DiscussionIsClosed();

        if (UserIds.Contains(userId))
            return Errors.Discussion.IsAlreadyParticipantError();*/
            
        UserIds.Add(userId);
        return Result.Success<Error>();
    }

    public UnitResult<Error> AddUsers(IEnumerable<UserId> userIds)
    {
        if (Status == Constants.DiscussionStatus.Close)
            return Errors.Discussion.DiscussionIsClosed();

        var usersToAdd = userIds.Where(u=> !UserIds.Contains(u)).ToList();
        
        UserIds.AddRange(usersToAdd);
        
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> DeleteMessage(Message message, UserId userId)
    {
         /*if (Status == Constants.DiscussionStatus.Close)
            return Errors.Discussion.DiscussionIsClosed();
         
       var isUserParticipatesInDiscussion = UserIds.Contains(userId);

        if (!isUserParticipatesInDiscussion)
            return Errors.Discussion.IsNotParticipantError();
        
        var isUsersParticipatesInMessage = message.UserId == userId;

        if (!isUsersParticipatesInMessage)
            return Errors.Message.MessageNotUser();*/
        
        Messages.Remove(message);
        
        return Result.Success<Error>();
    }
    
    public UnitResult<Error> DeleteMessages(IEnumerable<Message> messages, UserId userId)
    {
        /*if (Status == Constants.DiscussionStatus.Close)
            return Errors.Discussion.DiscussionIsClosed();
         
        var isUserParticipatesInDiscussion = UserIds.Contains(userId);

        if (!isUserParticipatesInDiscussion)
            return Errors.Discussion.IsNotParticipantError();
        
        var isUsersParticipatesInMessage = messages.All(m=> m.UserId == userId);

        if (!isUsersParticipatesInMessage)
            return Errors.Message.MessageNotUser();*/

        foreach (var message in messages)
        {
            Messages.Remove(message);
        }
        
        return Result.Success<Error>();
    }
}