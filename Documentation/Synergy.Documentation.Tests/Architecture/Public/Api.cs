using Synergy.Documentation.Annotations;
using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Architecture.Public;

[UsesVerify]
[CodeFilePath]
public class Api
{
    [Fact]
    public async Task Generate()
    {
        var assembly = typeof(ApiDescription).Assembly;
        var publicApi = ApiDescription.GenerateFor(assembly);
        
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName().Name);
    }
}