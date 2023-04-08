namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramSelfCallAttribute : Attribute, SequenceDiagramElement, SequenceDiagramGroup
{
    public string Method { get; }
    public string? Note { get; set; }
    public SequenceDiagramGroupType Group { get; set; }
    public string? GroupHeader { get; set; }
    
    public SequenceDiagramSelfCallAttribute(string method)
    {
        this.Method = method;
    }
}