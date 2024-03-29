﻿namespace Synergy.Behaviours.Testing.Gherkin;

public record Background(
    List<Step> Steps,
    Line Line
)
{
    public const string Keyword = "Background";
}