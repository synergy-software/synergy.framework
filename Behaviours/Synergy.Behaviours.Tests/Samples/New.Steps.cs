using Synergy.Behaviours.Testing;

namespace Synergy.Behaviours.Tests.Samples;

[UsesVerify]
public partial class NewFeature : Feature<NewFeature>
{
    [Fact]
    public async Task GenerateFeature()
    {
        var code = this.Generate(
            from: "New.feature"
        );

        await Verifier
              .Verify(code, "cs")
              .UseFileName("New.Feature")
              .AutoVerify();
    }
}