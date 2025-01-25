using System.Reflection;

namespace PetHelper.Species.Controllers;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}