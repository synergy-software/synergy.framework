using System;
using System.Diagnostics;

namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

[Conditional("CODE_ANALYSIS")]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SequenceDiagramDatabaseCallAttribute : SequenceDiagramExternalCallAttribute
{
    public SequenceDiagramDatabaseCallAttribute(string sql):base("Database", sql)
    {
        this.Archetype = SequenceDiagramArchetype.Database;
    }
}