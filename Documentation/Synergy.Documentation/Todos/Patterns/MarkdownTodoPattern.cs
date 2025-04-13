using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from Typescript files.
/// </summary>
public record MarkdownTodoPattern() : TodoPattern(
    "md",
    new Regex("\\[\\/\\/\\]\\:\\s+#\\s+\\((TODO.*?)\\)"), // [//]: # (TODO Write the documentation of Markdown class usage)
    match => match.Groups[1].Value
);