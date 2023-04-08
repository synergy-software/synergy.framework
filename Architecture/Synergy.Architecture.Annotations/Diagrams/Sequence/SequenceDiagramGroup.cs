namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

public interface SequenceDiagramGroup
{
    public SequenceDiagramGroupType Group { get; }
    public string? GroupHeader { get; }
}