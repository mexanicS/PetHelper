namespace PetHelper.Accounts.Contracts.Responses;

public record RefreshTokensRequest(string AccessToken, Guid RefreshToken);