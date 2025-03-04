namespace PetHelper.SharedKernel;

public class SoftDeleteConfig
{
    public int DaysToHardDelete { get; set; }
    
    public int HoursToCheck { get; init; }
}