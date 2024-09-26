using PetHelper.Domain.Shared;

namespace PetHelper.API;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope()
{
    public object? Result { get; }
    
    public DateTime? TimeGenerated { get; }

    public List<ResponseError> Errors { get; } = [];
    
    private Envelope(object? result, IEnumerable<ResponseError> errors) : this()
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenerated = DateTime.Now;
    }
    
    public static Envelope Ok(object? result = null) => 
        new (result, []);
    
    public static Envelope Error(IEnumerable<ResponseError> errors) => 
        new (null, errors);
}