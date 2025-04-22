using Synergy.Architecture.Annotations.Diagrams.Sequence;

namespace Synergy.Architecture.Diagrams.Sequence;

public class SequenceDiagramActor
{
    public SequenceDiagramActor(
        string Name,
        SequenceDiagramArchetype Archetype = SequenceDiagramArchetype.Actor,
        string? Note = null,
        string? Colour = null
        )
    {
        this.Name = Name;
        this.Archetype = Archetype;
        this.Note = Note;
        this.Colour = Colour;
    }

    internal string CodeName => this.Name.CodeName();
    public string Name { get; }
    public SequenceDiagramArchetype Archetype { get; }
    public string? Note { get; }
    public string? Colour { get; }

    public void Deconstruct(out string Name,
        out SequenceDiagramArchetype Archetype,
        out string? Note,
        out string? Colour)
    {
        Name = this.Name;
        Archetype = this.Archetype;
        Note = this.Note;
        Colour = this.Colour;
    }
}