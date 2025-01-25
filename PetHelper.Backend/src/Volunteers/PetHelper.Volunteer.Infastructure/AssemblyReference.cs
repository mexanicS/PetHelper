using System.Reflection;

namespace PetHelper.Volunteer.Infastructure;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}