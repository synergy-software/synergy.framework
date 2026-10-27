using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

// TODO: Marcin Celej [from: Marcin Celej on: 14-04-2023]: Add way to exclude some files from the scan - by path

/// <summary>
/// Represents a pattern for searching for TODO comments in a file.
/// The pattern is defined by a regular expression and an extractor function.
/// </summary>
/// <param name="FileExtension"></param>
/// <param name="Regex"></param>
/// <param name="TodoExtractor"></param>
public abstract record TodoPattern(
    string FileExtension,
    Regex Regex,
    Func<Match, string> TodoExtractor
)
{
    /// <summary>
    /// Finds matches in the content, using line-by-line matching by default.
    /// </summary>
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