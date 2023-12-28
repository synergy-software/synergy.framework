using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Architecture.Dependencies;

[UsesVerify]
public class Package
{
    [Theory]
    [InlineData(typeof(UsesVerifyAttribute))]
    [InlineData(typeof(TheoryAttribute))]
    public async Task Generate(Type type)
    {
        // ARRANGE
        var assembly = type.Assembly;

        // ACT
        var publicApi = ApiDescription.GenerateFor(assembly);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName().Name);
    }
}