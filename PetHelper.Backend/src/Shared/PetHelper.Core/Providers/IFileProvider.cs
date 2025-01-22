using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetHelper.Core.FileProvider;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using FileInfo = PetHelper.Core.FileProvider.FileInfo;

namespace PetHelper.Core.Providers;

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