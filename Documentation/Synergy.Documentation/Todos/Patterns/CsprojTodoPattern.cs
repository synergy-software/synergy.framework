using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

public record CsprojTodoPattern() : TodoPattern(
    "csproj",
    new Regex("(TODO.*)"),
    match => match.Groups[1].Value.Replace("-->", "").Trim()
);