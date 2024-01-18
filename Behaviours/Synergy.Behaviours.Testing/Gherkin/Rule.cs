namespace Synergy.Behaviours.Testing.Gherkin;

public record Rule(
    string Title,
    Background? Background,
    Line Line
)
{
    public const string Keyword = "Rule";
}