namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

// TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Add optional attributes when compilation constant CODE_ANALYSIS (or other) is present

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