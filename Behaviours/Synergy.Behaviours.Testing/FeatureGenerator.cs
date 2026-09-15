using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
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
        string? placeholder = null,
        [CallerFilePath] string callerFilePath = ""
    )
    {
        var code = feature.Generate(
            from,
            include,
            generateAfter,
            placeholder,
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
        string? placeholder = null,
        [CallerFilePath] string callerFilePath = ""
    )
    {
        if (featureClass == null)
            throw new ArgumentNullException(nameof(featureClass));

        var gherkin = GherkinReader.ReadAllLinesFrom(callerFilePath, from);
        var feature = GherkinParser.Parse(gherkin);
        var generator = new XUnitFeatureGenerator(include, generateAfter);
        var code = generator.Generate(feature, featureClass);
        
        GenerateMissingMethodPlaceholders(featureClass, callerFilePath, generator, placeholder);

        return code.ToString();
    }

    private static void GenerateMissingMethodPlaceholders<TBehaviour>(
        [DisallowNull] TBehaviour featureClass,
        string callerFilePath,
        XUnitFeatureGenerator generator,
        string? placeholder
    )
    {
        if (placeholder == null)
            return;
        
        var missingMethods = new StringBuilder();
        foreach (var methodName in generator.Methods.Distinct())
        {
            var method = featureClass.GetType()
                                     .GetMethod(methodName,
                                         System.Reflection.BindingFlags.Public |
                                         System.Reflection.BindingFlags.NonPublic |
                                         System.Reflection.BindingFlags.Instance
                                     );
            if (method == null)
            {
                missingMethods.AppendLine($"    private void {methodName}()");
                missingMethods.AppendLine("    {");
                missingMethods.AppendLine("        throw new NotImplementedException();");
                missingMethods.AppendLine("    }");
                missingMethods.AppendLine();
            }
        }

        missingMethods.Append(placeholder);

        var callerContent = File.ReadAllText(callerFilePath);
        var amended = callerContent.Replace(placeholder, missingMethods.ToString());
        File.WriteAllText(callerFilePath, amended);
    }
}