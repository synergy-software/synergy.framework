﻿using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

public record TextTodoPattern() : TodoPattern(
    "txt",
    new Regex("(TODO.*)"),
    match => match.Groups[1]
                  .Value
);