using CSharpFunctionalExtensions;
using PetHelper.Core.FileProvider;
using PetHelper.Core.Providers;
using PetHelper.SharedKernel;

namespace PetHelper.Core.File;

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