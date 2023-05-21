using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramExternalActivationAttribute : Attribute, SequenceDiagramElement, SequenceDiagramGroup
{
    public string Type { get; }
    public string? Constructor { get; }
    public string? Note { get; set; }
    public SequenceDiagramArchetype Archetype { get; set; }
    public SequenceDiagramGroupType Group { get; set; }
    public string? GroupHeader { get; set; }
    
    public SequenceDiagramExternalActivationAttribute(string type, string? constructor)
    {
        this.Type = type;
        this.Constructor = constructor;
        this.Archetype = SequenceDiagramArchetype.Participant;
    }
}