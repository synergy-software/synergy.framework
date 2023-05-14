using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

// TODO: Marcin Celej [from: Marcin Celej on: 14-04-2023]: Add way to exclude some files from the scan - by path

public abstract record TodoPattern(
    string FileExtension,
    Regex Regex,
    Func<Match, string> TodoExtractor
);