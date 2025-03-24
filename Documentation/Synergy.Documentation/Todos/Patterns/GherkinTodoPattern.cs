using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from Gherkin *.feature files.
/// </summary>
public record GherkinTodoPattern() : TodoPattern(
    "feature",
    new Regex("#\\s*(TODO.*)"),
    match => match.Groups[1].Value
);