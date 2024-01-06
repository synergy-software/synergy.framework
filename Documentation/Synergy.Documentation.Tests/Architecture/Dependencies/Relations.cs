using Synergy.Documentation.Api;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Architecture.Dependencies;

[UsesVerify]
public class Relations
{
    [Theory]
    [InlineData(typeof(Markdown))]
    [InlineData(typeof(Markdown.Document))]
    public async Task Generate(Type type)
    {
        // ARRANGE
        var dependencies = Synergy.Documentation.Api.Dependencies.Of(type);

        // ACT
        var publicApi = ApiDescription.GenerateFor(dependencies);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + type.Name);
    }
}