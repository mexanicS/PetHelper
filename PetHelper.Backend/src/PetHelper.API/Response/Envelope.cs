using PetHelper.Domain.Shared;

namespace PetHelper.API;

public record Envelope()
{
    public object? Result { get; }

    public string? ErrorCode { get; }
    
    public string? ErrorMessage { get; }
    
    public DateTime? TimeGenerated { get; }

    private Envelope(object? result, Error? error) : this()
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        TimeGenerated = DateTime.Now;
    }
    
    public static Envelope Ok(object? result = null) => 
        new (result, null);
    
    public static Envelope Error(Error error) => 
        new (null, error);
}