using System.Reflection;

namespace PetHelper.Volunteer.Controllers;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}