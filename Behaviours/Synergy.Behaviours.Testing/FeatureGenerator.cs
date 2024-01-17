using System.Runtime.CompilerServices;
using Synergy.Behaviours.Testing.Generator;
using Synergy.Behaviours.Testing.Gherkin;
using Synergy.Behaviours.Testing.Gherkin.File;
using Synergy.Behaviours.Testing.Gherkin.Parser;

namespace Synergy.Behaviours.Testing;

public static class FeatureGenerator
{
    public static void Generate<TBehaviour>(
        this TBehaviour feature,
        string from,
        string to,
        Func<Scenario, bool>? include = null,
        Func<Scenario, bool>? generateAfter = null,
        [CallerFilePath] string callerFilePath = ""
    )
    {
        var code = feature.Generate(
            from,
            include,
            generateAfter,
            // ReSharper disable once ExplicitCallerInfoArgument
            callerFilePath
        );

        GherkinWriter.Write(callerFilePath, to, code);
    }

    public static string Generate<TBehaviour>(
        this TBehaviour featureClass,
        string from,
        Func<Scenario, bool>? include = null,
        Func<Scenario, bool>? generateAfter = null,
        [CallerFilePath] string callerFilePath = ""
    )
    {
        if (featureClass == null)
            throw new ArgumentNullException(nameof(featureClass));

        var gherkin = GherkinReader.ReadAllLinesFrom(callerFilePath, from);
        var feature = GherkinParser.Parse(gherkin);
        var code = new XUnitFeatureGenerator(include, generateAfter).Generate(feature, featureClass);
        return code.ToString();
    }
}