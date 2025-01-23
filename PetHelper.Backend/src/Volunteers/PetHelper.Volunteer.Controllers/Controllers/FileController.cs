using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHelper.Core.File;
using PetHelper.Core.FileProvider;
using PetHelper.Framework;
using PetHelper.SharedKernel.ValueObjects;
using FileInfo = PetHelper.Core.FileProvider.FileInfo;

namespace PetHelper.Volunteer.Controllers.Controllers;

public class FileController : ApplicationController
{
    private readonly string BUCKET_NAME = "photos";
    
    [HttpPost]
    public async Task<IActionResult> UploadFile(
        IFormFile file, 
        [FromServices] AddFileHandler handler,
        CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();
        //ToDo
        var fileInfo = new FileInfo(FilePath.Create(Guid.NewGuid(), Path.GetExtension(file.FileName)).Value, BUCKET_NAME);
        
        var fileData = new FileData(stream, fileInfo);
        var result = await handler.Handle(fileData, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("{objectName:guid}")]
    public async Task<IActionResult> GetFileById(
        [FromRoute] Guid objectName,
        [FromServices] GetFileByNameHandler handler,
        CancellationToken cancellationToken)
    {
        var fileMetadata = new FileMetaData(BUCKET_NAME, objectName.ToString());
        var result = await handler.Handle(fileMetadata, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{objectName:guid}")]
    public async Task<IActionResult> RemoveFile(
        [FromRoute] Guid objectName,
        [FromServices] DeleteFileHandler handler,
        CancellationToken cancellationToken)
    {
        var fileMetadata = new FileMetaData(BUCKET_NAME, objectName.ToString());
        var result = await handler.Handle(fileMetadata, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
} 