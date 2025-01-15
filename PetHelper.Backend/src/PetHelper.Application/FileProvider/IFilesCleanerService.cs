namespace PetHelper.Application.FileProvider;

public interface IFilesCleanerService
{
    Task Process(CancellationToken stoppingToken);
}