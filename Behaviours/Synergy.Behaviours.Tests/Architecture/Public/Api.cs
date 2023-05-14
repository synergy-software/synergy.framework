using Synergy.Behaviours.Testing;
using Synergy.Documentation.Api;

namespace Synergy.Behaviours.Tests.Architecture.Public;

[UsesVerify]
public class Api
{
    [Fact]
    public async Task Generate()
    {
        // ARRANGE
        var assembly = typeof(Feature<>).Assembly;

        // ACT
        var publicApi = ApiDescription.GenerateFor(assembly);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName()
                                                      .Name);
    }
}