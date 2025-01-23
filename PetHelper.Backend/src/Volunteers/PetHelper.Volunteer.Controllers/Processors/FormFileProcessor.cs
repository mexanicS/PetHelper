using Microsoft.AspNetCore.Http;
using PetHelper.Core.DTOs.Pet;

namespace PetHelper.Volunteer.Controllers.Processors;

public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _filesList = [];

    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new UploadFileDto(file.FileName, stream);
            _filesList.Add(fileDto);
        }
        
        return _filesList;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var file in _filesList)
        {
            await file.Content.DisposeAsync();
        }
    }
}