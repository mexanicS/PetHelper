using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using Assembly = System.Reflection.Assembly;
namespace PetHelper.Accounts.ArchitectureTests;

public abstract class BaseTests
{
    protected static readonly Assembly PresentationAssembly = Controllers.AssemblyReference.Assembly;
    protected static readonly Assembly DomainAssembly = Domain.AssemblyReference.Assembly;
    protected static readonly Assembly ApplicationAssembly = Application.AssemblyReference.Assembly;
    protected static readonly Assembly InfrastructureAssembly = Infastructure.AssemblyReference.Assembly;
    protected static readonly Assembly ContractsAssembly = Contracts.AssemblyReference.Assembly;
}
public abstract class ArchUnitBaseTest : BaseTests
{
    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            PresentationAssembly,
            DomainAssembly,
            ApplicationAssembly,
            InfrastructureAssembly,
            ContractsAssembly)
        .Build();

    public static readonly IObjectProvider<IType> PresentationLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(PresentationAssembly).As("Presentation layer");

    public static readonly IObjectProvider<IType> DomainLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(DomainAssembly).As("Domain layer");

    public static readonly IObjectProvider<IType> ApplicationLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(ApplicationAssembly).As("Application layer");

    public static readonly IObjectProvider<IType> InfrastructureLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(InfrastructureAssembly).As("Infrastructure layer");

    public static readonly IObjectProvider<IType> ContractsLayer =
        ArchRuleDefinition.Types().That().ResideInAssembly(ContractsAssembly).As("Contracts layer");
}