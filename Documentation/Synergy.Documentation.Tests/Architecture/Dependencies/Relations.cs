using Synergy.Documentation.Api;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Architecture.Dependencies;

[UsesVerify]
public class Relations
{
    [Theory]
    [InlineData(typeof(Markdown))]
    public async Task Generate(params Type[] type)
    {
        // ARRANGE
        var dependencies = Synergy.Documentation.Api.Dependencies.Of(type.First(), includeNested: true);

        // ACT
        var publicApi = ApiDescription.GenerateFor(dependencies);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + type.First().Name);
    }
}