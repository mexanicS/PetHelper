namespace PetHelper.Core.DTOs;

public record FullNameDto(
    string FirstName, 
    string LastName, 
    string? MiddleName);