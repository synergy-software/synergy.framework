using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for searching for TODO comments in a file.
/// The pattern is defined by a regular expression and an extractor function.
/// </summary>
/// <param name="FileExtension">The file extension to scan, without a leading dot.</param>
/// <param name="Regex">The regular expression used to locate TODO comments.</param>
/// <param name="TodoExtractor">The function that extracts report text from a successful match.</param>
public abstract record TodoPattern(
    string FileExtension,
    Regex Regex,
    Func<Match, string> TodoExtractor
)
{
    /// <summary>
    /// Finds matches in the content, using line-by-line matching by default.
    /// </summary>
    /// <param name="content">The file contents to search.</param>
    /// <returns>At most one successful match per line, in source order.</returns>
    public virtual IEnumerable<Match> Matches(string content)
    {
        using var reader = new StringReader(content);
        while (reader.ReadLine() is { } line)
        {
            Match match = Regex.Match(line);
            if (match.Success)
                yield return match;
        }
    }
}