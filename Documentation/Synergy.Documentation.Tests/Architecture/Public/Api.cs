using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Architecture.Public;

[UsesVerify]
public class Api
{
    [Fact]
    public async Task Generate()
    {
        // ARRANGE
        var assembly = typeof(ApiDescription).Assembly;

        // ACT
        var publicApi = ApiDescription.GenerateFor(assembly);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName()
                                                      .Name);
    }
}