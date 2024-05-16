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
    public new static readonly string[] Keywords = { "Scenario Outline", "Scenario Template" };
}