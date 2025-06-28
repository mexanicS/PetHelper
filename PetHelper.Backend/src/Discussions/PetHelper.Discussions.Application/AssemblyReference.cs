using System.Reflection;

namespace PetHelper.Discussions.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}