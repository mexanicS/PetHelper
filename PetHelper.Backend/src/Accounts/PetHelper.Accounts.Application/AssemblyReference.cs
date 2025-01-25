using System.Reflection;

namespace PetHelper.Accounts.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}