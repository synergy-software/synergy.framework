using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

public record CsprojTodoPattern() : TodoPattern(
    "csproj",
    new Regex("<!--\\s*?(TODO.*?)\\s*?-->"),
    match => match.Groups[1].Value.Trim()
);