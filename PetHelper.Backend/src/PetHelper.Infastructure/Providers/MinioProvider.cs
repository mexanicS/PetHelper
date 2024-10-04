using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Providers;
using PetHelper.Domain.Shared;

namespace PetHelper.Infastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    
    public async Task<Result<string, Error>> UploadFile(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!await IsBucketExist(fileData.BucketName,cancellationToken))
                await CreateBucketAsync(fileData.BucketName, cancellationToken);
            
            var path = Guid.NewGuid();
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(path.ToString());
            
            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            
            _logger.LogInformation("The file was successfully uploaded to the {bucketName} folder on the minio", 
                fileData.BucketName);
            
            return result.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Fail to upload file in minio");
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }
    
    public async Task<Result<string, Error>> GetFileByObjectName(
        FileMetaData fileMetadata, 
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await IsBucketExist(fileMetadata.BucketName, cancellationToken)) 
                return Error.NotFound("bucket.not.found", "Bucket doesn`t exist in minio");
                
            var objectExistArgs = new PresignedGetObjectArgs()
                .WithBucket(fileMetadata.BucketName)
                .WithObject(fileMetadata.ObjectName)
                .WithExpiry(Constants.EXPIRY_IN_SECONDS);
            
            var objectUrl = await _minioClient.PresignedGetObjectAsync(objectExistArgs);
            
            if (string.IsNullOrWhiteSpace(objectUrl))
            {
                return Error.NotFound("file.not.found", "File doesn`t exist in minio");
            }
            
            _logger.LogInformation("File named {objectName} received", 
                fileMetadata.ObjectName);
            
            return objectUrl;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Fail to get file in minio");
            return Error.Failure("file.get", "Fail to get file in minio");
        }
    }
    
    public async Task<Result<string, Error>> DeleteFile(
        FileMetaData fileData, 
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await IsBucketExist(fileData.BucketName, cancellationToken)) 
                return Error.NotFound("bucket.not.found", "Bucket doesn`t exist in minio");
            
            var objectExistArgs = new PresignedGetObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName)
                .WithExpiry(Constants.EXPIRY_IN_SECONDS);
            
            var objectExist = await _minioClient.PresignedGetObjectAsync(objectExistArgs);
            
            if (string.IsNullOrWhiteSpace(objectExist))
            {
                return Error.NotFound("file.not.found", "File doesn`t exist in minio");
            }
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName);
            
            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
            
            _logger.LogInformation("File named {fileName} removed from {bucketName} bucket in minio", 
                fileData.ObjectName, fileData.BucketName);
            
            return fileData.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Fail to delete file in minio");
            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }
    
    private async Task<bool> IsBucketExist(string bucketName,CancellationToken cancellationToken)
    {
        var bucketExistArgs = new BucketExistsArgs()
            .WithBucket(bucketName);
    
        return await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
    }
    
    private async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
        await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        
        _logger.LogInformation("Bucket named {bucketName} was created in minio", 
            bucketName);
    }
}