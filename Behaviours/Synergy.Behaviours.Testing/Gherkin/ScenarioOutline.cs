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
    public new static string[] Keywords { get; } = { "Scenario Outline", "Scenario Template" };

    /// <inheritdoc />
    public override string[] Lines
    {
        get
        {
            var lines = new List<string>(base.Lines);
            // TODO: Marcin Celej [from: Marcin Celej on: 18-01-2024]: add here Examples section when there is deicated class for header and row
            return lines.ToArray();
        }
    }
}