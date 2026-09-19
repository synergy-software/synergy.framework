using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from YAML files.
/// It looks for lines that start with a '#' followed by optional whitespace and then the word 'TODO'.
/// </summary>
public record YamlTodoPattern() : TodoPattern(
    "yaml",
    new Regex("#\\s*(TODO.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
    match => match.Groups[1].Value
);