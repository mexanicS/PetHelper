using CSharpFunctionalExtensions;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.FileProvider;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Providers;

public interface IFileProvider
{
    public Task<Result<string, Error>> UploadFile(FileData fileData, CancellationToken cancellationToken);
    
    public Task<Result<string, Error>> DeleteFile(FileMetaData fileMetadata, CancellationToken cancellationToken);
    
    public Task<Result<string, Error>> GetFileByObjectName(FileMetaData fileMetadata, CancellationToken cancellationToken);

    public Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<UploadingFileDto> files,
        string bucketName,
        CancellationToken cancellationToken = default);
}