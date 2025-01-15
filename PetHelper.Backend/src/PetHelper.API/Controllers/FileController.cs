using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using PetHelper.API.Extensions;
using PetHelper.Application.File;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Providers;
using PetHelper.Domain.ValueObjects;
using PetHelper.Infastructure.Options;
using FileInfo = PetHelper.Application.FileProvider.FileInfo;

namespace PetHelper.API.Controllers;

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