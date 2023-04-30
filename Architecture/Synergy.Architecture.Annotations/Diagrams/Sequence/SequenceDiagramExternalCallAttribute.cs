﻿namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramExternalCallAttribute : Attribute, SequenceDiagramElement, SequenceDiagramGroup
{
    public string Type { get; }
    public string Method { get; }
    public string? Note { get; set; }
    public SequenceDiagramArchetype Archetype { get; set; }
    public SequenceDiagramGroupType Group { get; set; }
    public string? GroupHeader { get; set; }
    
    public SequenceDiagramExternalCallAttribute(string type, string method)
    {
        this.Type = type;
        this.Method = method;
        this.Archetype = SequenceDiagramArchetype.Participant;
    }
}