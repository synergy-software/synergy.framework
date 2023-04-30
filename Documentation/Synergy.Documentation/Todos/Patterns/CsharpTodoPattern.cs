using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos;

public record CsharpTodoPattern() : TodoPattern(
    "cs",
    new Regex("\\/\\/\\s*(TODO.*)"),
    match => match.Groups[1]
                  .Value
);