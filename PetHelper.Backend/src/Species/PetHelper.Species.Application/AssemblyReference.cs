using System.Reflection;

namespace PetHelper.Species.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}