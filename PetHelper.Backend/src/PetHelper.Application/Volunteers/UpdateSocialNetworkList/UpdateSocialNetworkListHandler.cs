using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.UpdateSocialNetworkList;

public class UpdateSocialNetworkListHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateSocialNetworkListHandler> _logger;

    public UpdateSocialNetworkListHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateSocialNetworkListHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        UpdateSocialNetworkListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var volunteerResult = await _volunteersRepository.
            GetVolunteerById(VolunteerId.Create(request.VolunteerId), cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var socialNetwork = new SocialNetworkList(
            request.UpdateSocialNetworkListRequestDto.SocialNetworkListDto.SocialNetworks
                .Select(c => SocialNetwork.Create(c.Name, c.Url).Value)
        );
        
        volunteerResult.Value.UpdateSocialNetwork(socialNetwork);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
            
        _logger.LogInformation("The list of social networks for user {volunteerId} has been updated", request.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}