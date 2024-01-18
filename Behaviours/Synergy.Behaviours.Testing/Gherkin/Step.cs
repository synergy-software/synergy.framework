namespace Synergy.Behaviours.Testing.Gherkin;

public record Step(
    string Type,
    string Text,
    Line Line
)
{
    public static string[] Keywords { get; } = { "Given", "When", "Then", "And", "But", "*" };
}