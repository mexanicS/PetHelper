using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Database;
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
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSocialNetworkListHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateSocialNetworkListHandler> logger,
        IValidator<UpdateSocialNetworkListCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
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
        
        await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("The list of social networks for user {volunteerId} has been updated", command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}