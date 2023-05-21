using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Architecture.Diagrams;
using Synergy.Architecture.Diagrams.Documentation;
using Synergy.Documentation.Api;

namespace Synergy.Architecture.Tests.Architecture.Public;

[UsesVerify]
public class Api
{
    [Theory]
    [InlineData(typeof(TechnicalBlueprint))]
    [InlineData(typeof(SequenceDiagramCallAttribute))]
    public async Task Generate(Type marker)
    {
        // ARRANGE
        var assembly = marker.Assembly;

        // ACT
        var publicApi = ApiDescription.GenerateFor(assembly);

        // ASSERT
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName()
                                                      .Name);
    }
}