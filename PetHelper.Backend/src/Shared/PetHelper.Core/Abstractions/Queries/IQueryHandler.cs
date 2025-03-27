using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;

namespace PetHelper.Core.Abstractions.Queries;

public interface IQueryHandler<TResponse,in TQuery> where TQuery : IQuery
{
    Task<Result<TResponse, ErrorList>> Handle(
        TQuery query, 
        CancellationToken cancellationToken);
}

public interface IQueryHandler<TResponse>
{
    public Task<TResponse> Handle(CancellationToken cancellationToken);
}
