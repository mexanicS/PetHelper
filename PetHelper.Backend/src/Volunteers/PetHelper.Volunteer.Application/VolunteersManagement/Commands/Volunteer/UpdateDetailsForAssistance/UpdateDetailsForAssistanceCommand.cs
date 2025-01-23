using PetHelper.Core.DTOs;
using ICommand = PetHelper.Core.Abstractions.Commands.ICommand;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateDetailsForAssistance;

public record UpdateDetailsForAssistanceCommand(Guid VolunteerId, 
    UpdateDetailsForAssistanceCommandDto UpdateDetailsForAssistanceCommandDto) : ICommand;

public record UpdateDetailsForAssistanceCommandDto(DetailsForAssistanceListDto DetailsForAssistanceListDto);