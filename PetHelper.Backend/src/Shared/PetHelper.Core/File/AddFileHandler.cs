using CSharpFunctionalExtensions;
using PetHelper.Core.FileProvider;
using PetHelper.Core.Providers;
using PetHelper.SharedKernel;

namespace PetHelper.Core.File;

public class AddFileHandler
{
    private readonly IFileProvider _fileProvider;

    public AddFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        FileData fileData, 
        CancellationToken cancellationToken)
    {
        return await _fileProvider.UploadFile(fileData, cancellationToken);
    }
}