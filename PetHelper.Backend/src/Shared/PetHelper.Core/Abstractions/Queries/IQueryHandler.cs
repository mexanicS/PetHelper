namespace PetHelper.Core.Abstractions.Queries;

public interface IQueryHandler<TResponse,in TQuery> where TQuery : IQuery
{
    Task<TResponse> Handle(
        TQuery query, 
        CancellationToken cancellationToken);
}