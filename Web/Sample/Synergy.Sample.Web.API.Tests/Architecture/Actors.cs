using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Architecture.Diagrams.Sequence;

namespace Synergy.Sample.Web.API.Tests.Architecture;

public static class Actors
{
    public static readonly SequenceDiagramActor Browser = new(
        "Browser",
        SequenceDiagramArchetype.Boundary,
        Note: "UI calling web api endpoints"
    );
}