using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

// TODO: Marcin Celej [from: Marcin Celej on: 21-05-2023]: Use this attribute in some sample

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramElementAttribute : Attribute, SequenceDiagramElement
{
    // TODO: Marcin Celej [from: Marcin Celej on: 14-07-2023]: Add Name here to allow custom name of participant
    public string? Note { get; set; }
    public SequenceDiagramArchetype Archetype { get; set; }
    public string? Colour { get; set; }
}