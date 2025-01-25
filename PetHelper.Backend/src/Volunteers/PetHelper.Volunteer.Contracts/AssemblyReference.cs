using System.Reflection;

namespace PetHelper.Volunteer.Contracts;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}