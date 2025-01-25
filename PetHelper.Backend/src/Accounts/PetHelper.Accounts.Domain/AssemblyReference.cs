using System.Reflection;

namespace PetHelper.Accounts.Domain;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}