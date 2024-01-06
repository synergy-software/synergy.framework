using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Architecture.Public;

[UsesVerify]
public class Package
{
    [Theory]
    [InlineData(typeof(UsesVerifyAttribute))]
    [InlineData(typeof(TheoryAttribute))]
    public async Task Generate(Type type)
    {
        var assembly = type.Assembly;
        var publicApi = ApiDescription.GenerateFor(assembly, includeAssemblyVersion: true);

        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName().Name);
    }
}