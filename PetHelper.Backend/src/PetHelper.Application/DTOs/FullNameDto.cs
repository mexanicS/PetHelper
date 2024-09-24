namespace PetHelper.Application.DTOs;

public record FullNameDto(
    string FirstName, 
    string LastName, 
    string? MiddleName);