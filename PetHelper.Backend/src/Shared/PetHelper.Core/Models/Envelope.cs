using PetHelper.SharedKernel;

namespace PetHelper.Core.Models;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope()
{
    public object? Result { get; }
    
    public DateTime? TimeGenerated { get; }

    public ErrorList? Errors { get; }
    
    private Envelope(object? result, ErrorList? errors) : this()
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }
    
    public static Envelope Ok(object? result = null) => 
        new (result, null);
    
    public static Envelope Error(ErrorList errors) => 
        new (null, errors);
}