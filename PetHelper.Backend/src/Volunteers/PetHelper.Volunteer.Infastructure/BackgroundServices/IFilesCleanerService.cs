namespace PetHelper.Volunteer.Infastructure.BackgroundServices;

public interface IFilesCleanerService
{
    Task Process(CancellationToken stoppingToken);
}