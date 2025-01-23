namespace PetHelper.Volunteer.Application;

public interface IFilesCleanerService
{
    Task Process(CancellationToken stoppingToken);
}