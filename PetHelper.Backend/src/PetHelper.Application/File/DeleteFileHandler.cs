using CSharpFunctionalExtensions;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Providers;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.File;

public class DeleteFileHandler
{
    private readonly IFileProvider _fileProvider;

    public DeleteFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        FileMetaData fileMetaData, 
        CancellationToken cancellationToken)
    {
        return await _fileProvider.DeleteFile(fileMetaData, cancellationToken);
    }
}