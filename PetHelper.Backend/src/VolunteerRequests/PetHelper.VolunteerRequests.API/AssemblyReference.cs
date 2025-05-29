using System.Reflection;

namespace PetHelper.VolunteerRequests.API;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}