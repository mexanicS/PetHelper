using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Extensions;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.Commands.UpdateSocialNetworkList;

public class UpdateSocialNetworkListHandler : ICommandHandler<Guid,UpdateSocialNetworkListCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateSocialNetworkListHandler> _logger;
    private readonly IValidator<UpdateSocialNetworkListCommand> _validator;

    public UpdateSocialNetworkListHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateSocialNetworkListHandler> logger,
        IValidator<UpdateSocialNetworkListCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworkListCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteersRepository.
            GetVolunteerById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var socialNetwork = new SocialNetworkList(
            command.UpdateSocialNetworkListCommandDto.SocialNetworkListDto.SocialNetworks
                .Select(c => SocialNetwork.Create(c.Name, c.Url).Value)
        );
        
        volunteerResult.Value.UpdateSocialNetwork(socialNetwork);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
            
        _logger.LogInformation("The list of social networks for user {volunteerId} has been updated", command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}