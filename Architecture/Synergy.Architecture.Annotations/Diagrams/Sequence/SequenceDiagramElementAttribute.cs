using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramElementAttribute : Attribute, SequenceDiagramElement
{
    public string? Note { get; set; }
    public SequenceDiagramArchetype Archetype { get; set; }
    public string? Colour { get; set; }
}