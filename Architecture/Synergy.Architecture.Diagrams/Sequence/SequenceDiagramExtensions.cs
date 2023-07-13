using System.Text.RegularExpressions;

namespace Synergy.Architecture.Diagrams.Sequence;

internal static class SequenceDiagramExtensions
{
    public static string CodeName(this string name)
        => Regex.Replace(name.Replace("\\n", ""), "[^a-zA-Z]", "_");
}