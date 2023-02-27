﻿using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Synergy.Behaviours.Testing;

public static class FeatureGenerator
{
    public static void Generate<TBehaviour>(
        this TBehaviour feature,
        string from,
        string to,
        bool withMoreover = false,
        [CallerFilePath] string callerFilePath = ""
    )
        where TBehaviour : IFeature
    {
        StringBuilder code = new StringBuilder();
        string className = feature.GetType().Name;
        var gherkinPath = Path.Combine(Path.GetDirectoryName(callerFilePath), from);
        var gherkins = File.ReadAllLines(gherkinPath);

        code.AppendLine("using System.CodeDom.Compiler;");
        code.AppendLine();
        code.AppendLine($"namespace {feature.GetType().Namespace};");
        code.AppendLine();
        code.AppendLine(
            $"[GeneratedCode(\"{typeof(FeatureGenerator).Assembly.FullName}\", \"{typeof(FeatureGenerator).Assembly.GetName().Version.ToString()}\")]");
        code.AppendLine($"public partial class {className}");
        code.AppendLine("{");
        // code.AppendLine("    [Fact]");
        // code.AppendLine("    public void Generate()");
        // code.AppendLine($"        => Behaviours<{featureClass}>.Generate({nameof(from)}: \"{from}\", this);");

        string? scenarioMethod = null;
        foreach (var line in gherkins)
        {
            if (line.Contains("#"))
            {
                //scenarioMethod = Moreover();
                code.AppendLine(line.Replace("#", "//"));
                continue;
            }

            if (string.IsNullOrEmpty(line.Trim()))
            {
                scenarioMethod = CloseScenario();
                code.AppendLine();
                continue;
            }

            var scenario = Regex.Match(line, "\\s*Scenario\\: (.*)");
            if (scenario.Success)
            {
                scenarioMethod = FeatureGenerator.ToMethod(scenario.Groups[1]
                                                                   .Value);
                code.AppendLine("    [Xunit.Fact]");
                code.AppendLine($"    public void {scenarioMethod}() // {line.Trim()}");
                code.AppendLine($"        => ");
            }

            var given = Regex.Match(line, "\\s*Given (.*)");
            if (given.Success)
                code.AppendLine($"            Given().{FeatureGenerator.ToMethod(given.Groups[1].Value)}()  // {line.Trim()}");

            var and = Regex.Match(line, "\\s*And (.*)");
            if (and.Success)
                code.AppendLine($"             .And().{FeatureGenerator.ToMethod(and.Groups[1].Value)}()  // {line.Trim()}");

            var but = Regex.Match(line, "\\s*But (.*)");
            if (but.Success)
                code.AppendLine($"             .But().{FeatureGenerator.ToMethod(but.Groups[1].Value)}()  // {line.Trim()}");

            var when = Regex.Match(line, "\\s*When (.*)");
            if (when.Success)
                code.AppendLine($"            .When().{FeatureGenerator.ToMethod(when.Groups[1].Value)}()  // {line.Trim()}");

            var then = Regex.Match(line, "\\s*Then (.*)");
            if (then.Success)
                code.AppendLine($"            .Then().{FeatureGenerator.ToMethod(then.Groups[1].Value)}() // {line.Trim()}");
        }

        CloseScenario();

        // code.AppendLine();
        // code.AppendLine($"    partial class {featureClass}");
        // code.AppendLine("    {");
        // code.AppendLine("    }");
        code.AppendLine("}");

        var destinationFilePath = Path.Combine(Path.GetDirectoryName(callerFilePath), to);
        File.WriteAllText(destinationFilePath, code.ToString());

        string? CloseScenario()
        {
            if (scenarioMethod != null)
            {
                if (withMoreover)
                    code.AppendLine($"            .Moreover().Verify{scenarioMethod}();");
                else
                    code.AppendLine($"            ;");
            }

            scenarioMethod = null;
            return scenarioMethod;
        }
    }

    private static string ToMethod(string sentence)
    {
        var parts = sentence.Split(" ");
        var m = string.Concat(parts.Where(p => !string.IsNullOrEmpty(p))
                                   .Select(p => p.Substring(0, 1)
                                                 .ToUpperInvariant() + p.Substring(1)));
        m = Regex.Replace(m, "[^A-Za-z0-9_]", "");
        return m;
    }
}