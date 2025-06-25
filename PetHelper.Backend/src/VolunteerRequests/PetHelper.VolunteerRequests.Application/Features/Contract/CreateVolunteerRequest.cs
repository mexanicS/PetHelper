using PetHelper.Accounts.Contracts.UserManagment;
using PetHelper.SharedKernel.ValueObjects.User;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;
using PetHelper.VolunteerRequests.Contracts;
using PetHelper.VolunteerRequests.Domain;

namespace PetHelper.VolunteerRequests.Application.Features.Contract;

public class CreateVolunteerRequestContract : ICreateVolunteerRequestContract
{
    private readonly ICreateUserContract _createUserContract;
    private readonly IGetRoleContract _getRoleContract;

    public CreateVolunteerRequestContract(
        ICreateUserContract createUserContract,
        IGetRoleContract getRoleContract
        )
    {
        _createUserContract = createUserContract; 
        _getRoleContract = getRoleContract;
    }
      
    public async Task<VolunteerRequestId> CreateVolunteerRequest(CancellationToken ct)
    {
        var roleId = _getRoleContract.GetRole("admin", CancellationToken.None).Result.Value;
        var userId = await _createUserContract.CreateUser(roleId, CancellationToken.None);
        var volunteerInfo = VolunteerInfo.Create("info").Value;

        var request = VolunteerRequest.Create(userId, volunteerInfo);
        
        var volunteerRequestId = VolunteerRequestId.Create(request.Id).Value;

        return volunteerRequestId;
    }
}