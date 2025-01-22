using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetHelper.Core.FileProvider;
using PetHelper.Core.Providers;
using PetHelper.SharedKernel;

namespace PetHelper.Core.File;

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