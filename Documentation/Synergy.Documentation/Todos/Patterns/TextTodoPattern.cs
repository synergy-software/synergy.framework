using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from *.txt files.
/// </summary>
public record TextTodoPattern() : TodoPattern(
    "txt",
    new Regex("(TODO.*)"),
    match => match.Groups[1].Value
);