using System.Reflection;

namespace PetHelper.Species.Infastructure;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}