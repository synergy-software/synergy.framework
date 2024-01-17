namespace Synergy.Behaviours.Testing.Gherkin.Tokenizer;

internal record GherkinToken(
    string Type,
    string Value,
    Line Line
);