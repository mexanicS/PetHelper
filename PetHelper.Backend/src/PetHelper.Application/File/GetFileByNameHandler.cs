using CSharpFunctionalExtensions;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Providers;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.File;

public class GetFileByNameHandler
{
    private readonly IFileProvider _fileProvider;

    public GetFileByNameHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        FileMetaData fileData, 
        CancellationToken cancellationToken)
    {
        return await _fileProvider.GetFileByObjectName(fileData, cancellationToken);
    }
}