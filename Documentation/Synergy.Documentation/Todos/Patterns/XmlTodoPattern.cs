using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from XML comments in project files.
/// </summary>
/// <param name="Extension">The file extension to scan.</param>
public record XmlTodoPattern(string Extension) : TodoPattern(
    Extension,
    new Regex("\\b(TODO[^\\r\\n]*?)(?=\\s*-->|\\r?$)", RegexOptions.Multiline),
    match => match.Groups[1].Value.Trim()
)
{
    private static readonly Regex commentRegex = new("<!--.*?-->", RegexOptions.Singleline);

    /// <summary>
    /// Finds TODOs only within XML comments, including comments that span multiple lines.
    /// </summary>
    public override IEnumerable<Match> Matches(string content)
    {
        foreach (Match comment in commentRegex.Matches(content))
        foreach (Match todo in Regex.Matches(comment.Value))
            yield return todo;
    }
}

/// <summary>
/// Represents a pattern for extracting TODOs from *.props files.
/// </summary>
public record PropsTodoPattern() : XmlTodoPattern("props");
