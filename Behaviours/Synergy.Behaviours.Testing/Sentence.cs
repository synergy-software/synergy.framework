using System.Text.RegularExpressions;

namespace Synergy.Behaviours.Testing;

// TODO: Marcin Celej [from: Marcin Celej on: 12-05-2023]: Add Public API verification test to see what is exposed from this library

internal static class Sentence
{
    public static string ToMethod(string sentence)
    {
        sentence = Regex.Replace(sentence, "[^A-Za-z0-9_]", " ");
        var parts = sentence.Split(" ");
        var words = parts.Where(word => !string.IsNullOrWhiteSpace(word))
                         .Select(word => word.Substring(0, 1)
                                             .ToUpperInvariant() + word.Substring(1)
                         );
        var method = string.Concat(words);
        return method;
    }

    public static string FromMethod(string text)
        => Regex.Replace(
                    text,
                    "([a-z])([A-Z])",
                    m => $"{m.Groups[1]} {m.Groups[2].Value.ToLowerInvariant()}"
                )
                .Trim();
}