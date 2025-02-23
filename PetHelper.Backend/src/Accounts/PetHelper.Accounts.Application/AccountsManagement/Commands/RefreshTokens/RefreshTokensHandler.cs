using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Application.AccountsManagement.Commands.Login;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Contracts.Responses;
using PetHelper.Accounts.Domain;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.RefreshTokens;

public class RefreshTokensHandler : ICommandHandler<LoginResponse, RefreshTokensCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokensHandler(IRefreshSessionManager refreshSessionManager,
        ITokenProvider tokenProvider,
        [FromKeyedServices(Constants.Context.AccountManagement)] IUnitOfWork unitOfWork
        )
    {
        _refreshSessionManager = refreshSessionManager;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        RefreshTokensCommand command, 
        CancellationToken cancellationToken)
    {
        var oldRefreshSession = await _refreshSessionManager
            .GetByRefreshToken(command.RefreshToken, cancellationToken);
        
        if(oldRefreshSession.IsFailure)
            return oldRefreshSession.Error;

        if (oldRefreshSession.Value.ExpiresIn < DateTime.UtcNow)
            return Errors.Token.ExpiredToken().ToErrorList();
        
        var userClaims = await _tokenProvider.GetUserClaims(command.AccessToken, cancellationToken);
        
        if(userClaims.IsFailure)
            return userClaims.Error.ToErrorList();
        
        var userIdString  = userClaims.Value.FirstOrDefault(uc=>uc.Type == CustomClaims.Id)?.Value;
        
        if(!Guid.TryParse(userIdString, out var userId))
            return Errors.General.Failure().ToErrorList();

        var userJtiString  = userClaims.Value.FirstOrDefault(uc=>uc.Type == CustomClaims.Jti)?.Value;
        
        if(!Guid.TryParse(userJtiString, out var userJtiGuid))
            return Errors.General.Failure().ToErrorList();
        
        if (oldRefreshSession.Value.UserId != userId)
            return Errors.Token.InvalidToken().ToErrorList();
        
        if (oldRefreshSession.Value.Jti != userJtiGuid)
            return Errors.Token.InvalidToken().ToErrorList();
        
        _refreshSessionManager.Delete(oldRefreshSession.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var accessToken = await _tokenProvider
            .GetAccessToken(oldRefreshSession.Value.User, cancellationToken );
        
        var refreshToken = await _tokenProvider
            .GenerateRefreshToken(oldRefreshSession.Value.User, accessToken.Jti, cancellationToken);
        
        return new LoginResponse(accessToken.AccessToken, refreshToken);
    }
}