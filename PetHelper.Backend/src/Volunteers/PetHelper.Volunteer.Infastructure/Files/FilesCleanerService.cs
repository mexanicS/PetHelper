using Microsoft.Extensions.Logging;
using PetHelper.Core.Messaging;
using PetHelper.Core.Providers;
using PetHelper.Volunteer.Application;
using FileInfo = PetHelper.Core.FileProvider.FileInfo;

namespace PetHelper.Volunteer.Infastructure.Files;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly ILogger<FilesCleanerService> _logger;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IFileProvider _fileProvider;
    public FilesCleanerService(IFileProvider fileProvider, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue, 
        ILogger<FilesCleanerService> logger)
    {
        _fileProvider = fileProvider;
        _messageQueue = messageQueue;
        _logger = logger;
    }

    public async Task Process(CancellationToken stoppingToken)
    {
        var fileInfos = await _messageQueue.ReadAsync(stoppingToken);

        foreach (var fileInfo in fileInfos)
        {
            await _fileProvider.RemoveFile(fileInfo, stoppingToken);
                
            _logger.LogInformation("File {fileInfo} removed in {bucketName}", 
                fileInfo.FilePath.Value, 
                fileInfo.BucketName);
        }
    }
}