using System.Reflection;

namespace PetHelper.VolunteerRequests.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}