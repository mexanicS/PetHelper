using System.Reflection;

namespace PetHelper.VolunteerRequests.Domain;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}