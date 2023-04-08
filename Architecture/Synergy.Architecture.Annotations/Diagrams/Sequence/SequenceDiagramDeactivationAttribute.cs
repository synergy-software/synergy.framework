namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

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