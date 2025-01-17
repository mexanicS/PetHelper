using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.DTOs;

namespace PetHelper.Application.Volunteers.Commands.UpdateDetailsForAssistance;

public record UpdateDetailsForAssistanceCommand(Guid VolunteerId, 
    UpdateDetailsForAssistanceCommandDto UpdateDetailsForAssistanceCommandDto) : ICommand;

public record UpdateDetailsForAssistanceCommandDto(DetailsForAssistanceListDto DetailsForAssistanceListDto);