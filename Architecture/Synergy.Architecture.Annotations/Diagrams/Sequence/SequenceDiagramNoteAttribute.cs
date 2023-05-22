using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

// TODO: Marcin Celej [from: Marcin Celej on: 21-05-2023]: Use this attribute in some sample

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