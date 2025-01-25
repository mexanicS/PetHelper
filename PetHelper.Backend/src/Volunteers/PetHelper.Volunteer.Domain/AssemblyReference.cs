using System.Reflection;

namespace PetHelper.Volunteer.Domain;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}