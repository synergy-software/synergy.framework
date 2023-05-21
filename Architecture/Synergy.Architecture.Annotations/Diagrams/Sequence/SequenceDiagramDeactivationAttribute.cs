using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramDeactivationAttribute : Attribute, SequenceDiagramElement
{
    public Type Type { get; }
    public string? Note { get; set; }

    public SequenceDiagramDeactivationAttribute(Type type)
    {
        this.Type = type;
    }
}