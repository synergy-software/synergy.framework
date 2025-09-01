using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from C# files.
/// </summary>
public record CsharpTodoPattern() : TodoPattern(
    "cs",
    new Regex("\\/\\/\\s*(TODO.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
    match => match.Groups[1].Value
);