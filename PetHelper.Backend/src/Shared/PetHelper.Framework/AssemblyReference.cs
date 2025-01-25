using System.Reflection;

namespace PetHelper.Framework;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}