using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Extracts TODOs from Markdown files using the comment form <c>[//]: # (TODO ...)</c>.
/// </summary>
public record MarkdownTodoPattern() : TodoPattern(
    "md",
    new Regex("\\[\\/\\/\\]\\:\\s+#\\s+\\((TODO.*?)\\)"), // [//]: # (TODO Write the documentation of Markdown class usage)
    match => match.Groups[1].Value
);