using System.Reflection;

namespace PetHelper.Accounts.Controllers;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}