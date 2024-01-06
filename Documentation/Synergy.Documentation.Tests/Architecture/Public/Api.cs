using Synergy.Documentation.Annotations;
using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Architecture.Public;

[UsesVerify]
[CodeFilePath]
public class Api
{
    [Theory]
    [InlineData(typeof(ApiDescription))]
    [InlineData(typeof(CodeFilePathAttribute))]
    public async Task Generate(Type representative)
    {
        var assembly = representative.Assembly;
        var publicApi = ApiDescription.GenerateFor(assembly);
        
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName().Name);
    }
}