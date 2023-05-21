using Synergy.Documentation.Api;
using Synergy.Web.Api.Testing;

namespace Synergy.Web.Api.Tests.Architecture.Public;

[UsesVerify]
public class Api
{
    [Fact]
    public async Task Generate()
    {
        // ARRANGE
        var assembly = typeof(TestServer).Assembly;

        // ACT
        var publicApi = ApiDescription.GenerateFor(assembly);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName()
                                                      .Name);
    }
}