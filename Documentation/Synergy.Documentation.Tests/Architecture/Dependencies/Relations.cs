using Synergy.Documentation.Annotations;
using Synergy.Documentation.Api;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Architecture.Dependencies;

[UsesVerify]
[CodeFilePath]
public class Relations
{
    [Theory]
    [InlineData(typeof(Markdown))]
    public async Task Generate(Type type)
    {
        var dependencies = Synergy.Documentation.Api.Dependencies.Of(type, includeNested: true);
        var publicApi = ApiDescription.GenerateFor(dependencies);

        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + type.Name);
    }
}