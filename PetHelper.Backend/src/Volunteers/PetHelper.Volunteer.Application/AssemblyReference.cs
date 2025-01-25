using System.Reflection;

namespace PetHelper.Volunteer.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}