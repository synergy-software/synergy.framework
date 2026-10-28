using System.Reflection;
using System.Reflection.Emit;
using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Api;

public class ReflectionNullabilityTests
{
    [Fact]
    public void GlobalNamespaceTypesCanBeDocumentedAndExplored()
    {
        var assembly = AssemblyBuilder.DefineDynamicAssembly(
            new AssemblyName("DocumentationGlobalNamespaceTests"), AssemblyBuilderAccess.Run);
        var module = assembly.DefineDynamicModule("Main");
        var builder = module.DefineType("GlobalType", TypeAttributes.Public);
        var type = builder.CreateType() ?? throw new InvalidOperationException("Failed to create test type.");

        var description = ApiDescription.GenerateFor(new[] { type }, assemblyName: "Example");

        Assert.Contains("## GlobalType (class)", description);
        Assert.Equal(new[] { type }, Dependencies.Of(type));
    }

    [Fact]
    public void StaticMethodsWithoutDeclaringTypesCanBeDescribed()
    {
        var assembly = AssemblyBuilder.DefineDynamicAssembly(
            new AssemblyName("DocumentationGlobalMethodTests"), AssemblyBuilderAccess.Run);
        var module = assembly.DefineDynamicModule("Main");
        var builder = module.DefineGlobalMethod(
            "Standalone", MethodAttributes.Public | MethodAttributes.Static, typeof(void), Type.EmptyTypes);
        builder.GetILGenerator().Emit(OpCodes.Ret);
        module.CreateGlobalFunctions();
        var method = module.GetMethod("Standalone")
                     ?? throw new InvalidOperationException("Failed to create test method.");

        Assert.Equal("Standalone() : void", ApiDescription.For(method, withAttributes: false));
    }
}
