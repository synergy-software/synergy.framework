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


        // TODO: Marcin Celej [from: Marcin Celej on: 26-12-2023]: Doe not work for Markdown.Document
        // ACT
        var publicApi = ApiDescription.GenerateFor(dependencies);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + type.Name);
    }
}