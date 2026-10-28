using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from YAML files.
/// Matches a '#' followed by optional whitespace and 'TODO', ignoring case, anywhere on a line.
/// </summary>
public record YamlTodoPattern() : TodoPattern(
    "yaml",
    new Regex("#\\s*(TODO.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
    match => match.Groups[1].Value
);