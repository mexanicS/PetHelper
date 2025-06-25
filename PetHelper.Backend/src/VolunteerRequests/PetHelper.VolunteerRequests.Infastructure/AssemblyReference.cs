using System.Reflection;

namespace PetHelper.VolunteerRequests.Infastructure;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}