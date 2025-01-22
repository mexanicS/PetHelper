using CSharpFunctionalExtensions;
using PetHelper.Accounts.Domain.AccountModels;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.Interfaces;

public interface IAccountManager
{
    public Task<UnitResult<ErrorList>> CreateAdminAccount(AdminAccount adminAccount);
    
    public Task<UnitResult<ErrorList>> CreateParticipantAccount(ParticipantAccount adminAccount);
    
    public Task<UnitResult<ErrorList>> CreateVolunteerAccount(VolunteerAccount adminAccount);
}