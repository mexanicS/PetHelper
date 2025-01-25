using System.Reflection;

namespace PetHelper.Core;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}