using CSharpFunctionalExtensions;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Providers;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.File;

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