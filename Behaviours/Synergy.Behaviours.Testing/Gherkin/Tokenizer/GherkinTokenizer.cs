using System.Text.RegularExpressions;

namespace Synergy.Behaviours.Testing.Gherkin.Tokenizer;

internal static class GherkinTokenizer
{
    public static IEnumerable<GherkinToken> Tokenize(string[] lines)
    {
        foreach (var line in Lines(lines))
        {
            if (string.IsNullOrWhiteSpace(line.Text))
                continue;

            Regex tagsRegex = new Regex("@(\\S+)+");
            foreach (Match tag in tagsRegex.Matches(line.Text))
                yield return new GherkinToken("@", tag.Groups[1].Value, line);

            Regex commentRegex = new Regex("^\\s*#(.*)");
            if (commentRegex.IsMatch(line.Text))
            {
                var match = commentRegex.Match(line.Text);
                yield return new GherkinToken("#", match.Groups[2].Value, line);
            }

            Regex featureRegex = new Regex("^\\s*?(Feature|Rule|Background|Scenario|Example|Scenario Outline|Scenario Template|Examples):\\s*?(.*)$", RegexOptions.Multiline);
            if (featureRegex.IsMatch(line.Text))
            {
                var match = featureRegex.Match(line.Text);
                yield return new GherkinToken(match.Groups[1].Value, match.Groups[2].Value, line);
            }

            Regex sentenceRegex = new Regex("^\\s*?(Given|When|Then|And|But|\\*)\\s+?(.+)$", RegexOptions.Multiline);
            if (sentenceRegex.IsMatch(line.Text))
            {
                var match = sentenceRegex.Match(line.Text);
                yield return new GherkinToken(match.Groups[1].Value, match.Groups[2].Value, line);
            }
        }
    }

    private static IEnumerable<Line> Lines(string[] lines)
    {
        var lineNo = 1;
        foreach (string line in lines)
        {
            yield return new Line(lineNo++, line);
        }
    }
}