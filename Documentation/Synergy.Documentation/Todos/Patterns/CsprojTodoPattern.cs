using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from *.csproj files.
/// </summary>
public record CsprojTodoPattern() : TodoPattern(
    "csproj",
    new Regex("<!--\\s*?(TODO.*?)\\s*?-->"),
    match => match.Groups[1].Value.Trim()
);