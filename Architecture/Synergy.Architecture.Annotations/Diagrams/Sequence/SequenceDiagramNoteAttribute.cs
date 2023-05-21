using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramNoteAttribute : Attribute, SequenceDiagramElement
{
    public string? Note { get; set; }

    public SequenceDiagramNoteAttribute(string note)
    {
        this.Note = note;
    }
}