using CSharpFunctionalExtensions;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.FileProvider;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using FileInfo = PetHelper.Application.FileProvider.FileInfo;

namespace PetHelper.Application.Providers;

public interface IFileProvider
{
    public Task<Result<string, Error>> UploadFile(
        FileData fileData, 
        CancellationToken cancellationToken);
    
    public Task<Result<string, Error>> DeleteFile(
        FileMetaData fileMetaData, 
        CancellationToken cancellationToken);
    
    public Task<Result<string, Error>> GetFileByObjectName(
        FileMetaData fileMetadata, 
        CancellationToken cancellationToken);

    public Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> files,
        string bucketName,
        CancellationToken cancellationToken = default);

    public Task<UnitResult<Error>> RemoveFile(
        FileInfo fileInfo,
        CancellationToken cancellationToken);
}