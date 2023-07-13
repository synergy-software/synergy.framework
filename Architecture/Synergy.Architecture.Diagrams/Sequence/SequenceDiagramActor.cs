using Synergy.Architecture.Annotations.Diagrams.Sequence;

namespace Synergy.Architecture.Diagrams.Sequence;

public record SequenceDiagramActor(
    string Name,
    SequenceDiagramArchetype Archetype = SequenceDiagramArchetype.Actor,
    string? Note = null,
    string? Colour = null
)
{
    internal string CodeName => this.Name.CodeName();
}