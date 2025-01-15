using Microsoft.Extensions.Logging;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Messaging;
using PetHelper.Application.Providers;
using FileInfo = PetHelper.Application.FileProvider.FileInfo;

namespace PetHelper.Infastructure.Files;

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