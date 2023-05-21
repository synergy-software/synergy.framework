using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramActivationAttribute : Attribute, SequenceDiagramElement, SequenceDiagramGroup
{
    public Type Type { get; }
    public string? Note { get; set; }
    public SequenceDiagramArchetype Archetype { get; set; }
    public SequenceDiagramGroupType Group { get; set; }
    public string? GroupHeader { get; set; }
    
    public SequenceDiagramActivationAttribute(Type type)
    {
        this.Type = type;
        this.Archetype = SequenceDiagramArchetype.Participant;
    }
}