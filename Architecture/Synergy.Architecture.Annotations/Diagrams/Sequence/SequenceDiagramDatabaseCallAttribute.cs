namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramDatabaseCallAttribute : SequenceDiagramExternalCallAttribute
{
    public SequenceDiagramDatabaseCallAttribute(string sql):base("Database", sql)
    {
        this.Archetype = SequenceDiagramArchetype.Database;
    }
}