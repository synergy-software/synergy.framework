using System;
using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramExternalCallAttribute : Attribute, SequenceDiagramElement, SequenceDiagramGroup
{
    public string Type { get; }
    public string Method { get; }
    public string? Result { get; }
    
    public string? Note { get; set; }
    public SequenceDiagramArchetype Archetype { get; set; }
    public SequenceDiagramGroupType Group { get; set; }
    public string? GroupHeader { get; set; }
    
    public SequenceDiagramExternalCallAttribute(string type, string method, string? result = null)
    {
        this.Type = type;
        this.Method = method;
        this.Archetype = SequenceDiagramArchetype.Participant;
        this.Result = result;
    }
}