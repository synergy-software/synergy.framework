using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from Typescript files.
/// </summary>
public record TypescriptTodoPattern() : TodoPattern(
    "ts",
    new Regex("\\/\\/\\s*(TODO.*)"),
    match => match.Groups[1].Value
);