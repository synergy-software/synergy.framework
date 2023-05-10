﻿using Synergy.Behaviours.Testing;

namespace Synergy.Behaviours.Tests.Samples;

public partial class NewFeature : Feature<NewFeature>
{
    [Fact]
    public void GenerateFeature()
    {
        this.Generate(
            from: "New.feature",
            to: "New.Feature.cs"
        );
    }
}