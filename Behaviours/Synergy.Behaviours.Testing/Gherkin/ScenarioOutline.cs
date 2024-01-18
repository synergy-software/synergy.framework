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
    public override string[] Lines
    {
        get
        {
            var lines = new List<string>();
            lines.AddRange(base.Lines);
            lines.Add(this.Examples.Line.Text);
            lines.Add(this.Examples.Header.Line.Text);
            lines.AddRange(this.Examples.Rows.Select(row => row.Line.Text));
            return lines.ToArray();
        }
    }
}