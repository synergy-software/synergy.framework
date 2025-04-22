namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

public interface SequenceDiagramGroup
{
    SequenceDiagramGroupType Group { get; }
    string? GroupHeader { get; }
}