using CSharpFunctionalExtensions;
using PetHelper.Application.FileProvider;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(FileData fileData, CancellationToken cancellationToken);
    
    Task<Result<string, Error>> DeleteFile(FileMetaData fileMetadata, CancellationToken cancellationToken);
    
    Task<Result<string, Error>> GetFileByObjectName(FileMetaData fileMetadata, CancellationToken cancellationToken);
}