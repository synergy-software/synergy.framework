namespace Synergy.Behaviours.Testing.Gherkin;

public record Feature(
    string Title,
    List<string> Tags,
    Background? Background,
    List<Scenario> Scenarios,
    Line Line
);