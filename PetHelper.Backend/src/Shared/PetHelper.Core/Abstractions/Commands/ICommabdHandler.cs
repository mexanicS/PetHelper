using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;

namespace PetHelper.Core.Abstractions.Commands;

public interface ICommandHandler<TResponse,in TCommand> where TCommand : ICommand
{
    Task<Result<TResponse, ErrorList>> Handle(
        TCommand command, 
        CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<UnitResult<ErrorList>> Handle(
        TCommand command, 
        CancellationToken cancellationToken);
}