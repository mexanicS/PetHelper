using System.Reflection;

namespace PetHelper.Accounts.Infastructure;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}