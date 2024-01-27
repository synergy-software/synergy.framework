namespace Synergy.Behaviours.Testing.Gherkin;

public record ScenarioOutline(
    string Title,
    List<string> Tags,
    List<Step> Steps,
    Rule? Rule,
    Examples Examples,
    Line Line
) : Scenario(
    Title,
    Tags,
    Steps,
    Rule,
    Line
)
{
    public new static string[] Keywords = { "Scenario Outline", "Scenario Template" };

    /// <inheritdoc />
    internal override List<string> Lines
    {
        get
        {
            var lines = new List<string>();

            foreach (var line in base.Lines)
            {
                var tweakedLine = line;
                foreach (var argument in this.Examples.Header.Values)
                {
                    var argumentName = Sentence.ToArgument(argument);
                    tweakedLine = tweakedLine.Replace($"<{argument}>", "<{" + argumentName + "}>");
                }

                lines.Add(tweakedLine);
            }

            lines.Add(this.Examples.Line.Text);
            lines.Add("        | {" + string.Join("} | {", this.Examples.Header.Values.Select(argument => Sentence.ToArgument(argument))) + "} |");
            return lines;
        }
    }
}