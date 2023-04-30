using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos;

public record GherkinTodoPattern() : TodoPattern(
    "feature",
    new Regex("#\\s*(TODO.*)"),
    match => match.Groups[1]
                  .Value
);