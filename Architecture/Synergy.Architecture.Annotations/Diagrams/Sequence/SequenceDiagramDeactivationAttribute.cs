using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

// TODO: Marcin Celej [from: Marcin Celej on: 21-05-2023]: Use this attribute in some sample

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