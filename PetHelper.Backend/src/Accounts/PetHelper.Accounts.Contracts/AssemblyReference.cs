using System.Reflection;

namespace PetHelper.Accounts.Contracts;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}