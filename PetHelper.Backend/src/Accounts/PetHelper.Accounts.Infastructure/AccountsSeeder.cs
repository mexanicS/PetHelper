using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Infastructure.DataSeeding;

namespace PetHelper.Accounts.Infastructure;

public class AccountsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AccountsSeeder(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task SeedAsync()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        
        var service = scope.ServiceProvider.GetRequiredService<AccountsSeederService>();  
        
        await service.SeedAsync();
    }
}