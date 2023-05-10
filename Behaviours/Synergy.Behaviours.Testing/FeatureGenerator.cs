﻿using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Synergy.Behaviours.Testing;

public static class FeatureGenerator
{
    private const string Background = nameof(Feature<object>.Background);
    private const string Given = nameof(Feature<object>.Given);
    private const string When = nameof(Feature<object>.When);
    private const string Then = nameof(Feature<object>.Then);
    private const string And = nameof(Feature<object>.And);
    private const string But = nameof(Feature<object>.But);
    private const string Moreover = nameof(Feature<object>.Moreover);

    // TODO: Marcin Celej [from: Marcin Celej on: 10-05-2023]: Add include / exclude as functions 
    public static void Generate<TBehaviour>(
        this TBehaviour feature,
        string from,
        string to,
        string[]? include = null,
        string[]? exclude = null,
        Func<Scenario, bool>? generateAfter = null,
        [CallerFilePath] string callerFilePath = ""
    )
    {
        var code = feature.Generate(
            from,
            include,
            exclude,
            generateAfter,
            callerFilePath
        );
        var destinationFilePath = Path.Combine(Path.GetDirectoryName(callerFilePath), to);
        File.WriteAllText(destinationFilePath, code);
    }

    public static string Generate<TBehaviour>(
        this TBehaviour featureClass,
        string from,
        string[]? include = null,
        string[]? exclude = null,
        Func<Scenario, bool>? generateAfter = null,
        [CallerFilePath] string callerFilePath = ""
    )
    {
        if (featureClass == null)
            throw new ArgumentNullException(nameof(featureClass));
        
        if (from == null)
            throw new ArgumentNullException(nameof(from));

        StringBuilder code = new StringBuilder();
        string className = featureClass.GetType()
                                       .Name;
        var gherkinPath = Path.Combine(Path.GetDirectoryName(callerFilePath), from);

        string[] gherkins = FeatureGenerator.ReadAllLinesFrom(gherkinPath);

        code.AppendLine("// <auto-generated />");
        code.AppendLine("using System.CodeDom.Compiler;");
        code.AppendLine();
        code.AppendLine($"namespace {featureClass.GetType().Namespace};");
        code.AppendLine();
        code.AppendLine(
            $"[GeneratedCode(\"{typeof(FeatureGenerator).Assembly.FullName}\", " +
            $"\"{typeof(FeatureGenerator).Assembly.GetName().Version.ToString()}\")]"
        );
        code.AppendLine($"public partial class {className}");
        code.AppendLine("{");

        Scenario? scenario = null;
        bool includeScenario = ResetInclude();
        List<string>? tags = null;
        int lineNo = 0;
        string? backgroundMethod = null;
        string? backgroundStarted = null;
        string? featureName = null;
        string? ruleName = null;

        foreach (var line in gherkins)
        {
            lineNo++;

            var comment = Regex.Match(line, "\\s*#(.*)");
            if (comment.Success)
            {
                tags = null;
                includeScenario = ResetInclude();
                //code.AppendLine(line.Replace("#", "//"));
                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                tags = null;
                includeScenario = ResetInclude();

                continue;
            }

            var feature = Regex.Match(line, "\\s*Feature\\: (.*)");
            if (feature.Success)
            {
                featureName = feature.Groups[1]
                                     .Value;
                continue;
            }

            var rule = Regex.Match(line, "\\s*Rule\\: (.*)");
            if (rule.Success)
            {
                CloseBackground();
                CloseScenario();

                ruleName = rule.Groups[1]
                               .Value;

                code.AppendLine($"    // {line.Trim()}");
                code.AppendLine();

                continue;
            }

            var background = Regex.Match(line, "\\s*Background\\:");
            if (background.Success)
            {
                CloseScenario();

                backgroundMethod = FeatureGenerator.ToMethod(ruleName ?? featureName ?? "Feature") + "Background";
                backgroundStarted = backgroundMethod;
                code.AppendLine($"    private void {backgroundMethod}() // {line.Trim()}");
                code.AppendLine("    {");
                continue;
            }

            // TODO: Marcin Celej [from: Marcin Celej on: 10-05-2023]: Support Scenario Outline along with Examples  
            
            var outline = Regex.Match(line, "\\s*Scenario (Outline|Template)\\: (.*)");
            if (outline.Success)
            {
                throw new NotSupportedException($"Scenario Outline keyword is not supported\nLine {lineNo}: {line.Trim()}");
            }

            var example = Regex.Match(line, "\\s*(Examples|Scenarios)\\: (.*)");
            if (example.Success)
            {
                throw new NotSupportedException($"Examples keyword is not supported\nLine {lineNo}: {line.Trim()}");
            }

            if (line.Trim()
                    .StartsWith("@"))
            {
                CloseScenario();

                tags = Regex.Matches(line, "\\@\\w+")
                            .Select(m => m.Value)
                            .ToList();

                if (include != null)
                    includeScenario = tags.Intersect(include)
                                          .Any();

                if (exclude != null)
                    includeScenario = !tags.Intersect(exclude)
                                           .Any();

                // if (include)
                //     code.AppendLine($"    [Xunit.Trait({string.Join(", ", tags.Select(t => "\"" + t.TrimStart('@') + "\""))})]");
                continue;
            }

            if (includeScenario == false && backgroundStarted == null)
                continue;

            var scenarioMatch = Regex.Match(line, "\\s*Scenario\\: (.*)");
            if (scenarioMatch.Success)
            {
                CloseBackground();
                CloseScenario();

                scenario = new Scenario(scenarioMatch.Groups[1].Value, (tags ?? new List<string>(0)).AsReadOnly());
                
                code.AppendLine("    [Xunit.Fact]");
                if (tags != null)
                    code.AppendLine($"    // {String.Join(" ", tags)}");
                code.AppendLine($"    public void {scenario.Method}() // {line.Trim()}");
                code.AppendLine("    {");
                var backgroundCall = "";
                if (backgroundMethod != null)
                    backgroundCall = $".{backgroundMethod}()";
                code.AppendLine($"       {Background}(){backgroundCall};");
                code.AppendLine();

                continue;
            }

            var given = Regex.Match(line, "\\s*Given (.*)");
            if (given.Success)
            {
                // if (backgroundStarted == null)
                // {
                //     code.Append($"                ");
                // }
                // else
                // {
                //     code.Append($"     ");
                // }

                code.AppendLine($"       {Given}().{FeatureGenerator.ToMethod(given.Groups[1].Value)}();  // {line.Trim()}");
                continue;
            }

            var and = Regex.Match(line, "\\s*And (.*)");
            if (and.Success)
            {
                code.AppendLine($"         {And}().{FeatureGenerator.ToMethod(and.Groups[1].Value)}();  // {line.Trim()}");
                continue;
            }

            var asterisk = Regex.Match(line, "\\s*\\* (.*)");
            if (asterisk.Success)
            {
                code.AppendLine($"         {And}().{FeatureGenerator.ToMethod(asterisk.Groups[1].Value)}();  // {line.Trim()}");
                continue;
            }

            var but = Regex.Match(line, "\\s*But (.*)");
            if (but.Success)
            {
                code.AppendLine($"         {But}().{FeatureGenerator.ToMethod(but.Groups[1].Value)}();  // {line.Trim()}");
                continue;
            }

            var when = Regex.Match(line, "\\s*When (.*)");
            if (when.Success)
            {
                code.AppendLine($"        {When}().{FeatureGenerator.ToMethod(when.Groups[1].Value)}();  // {line.Trim()}");
                continue;
            }

            var then = Regex.Match(line, "\\s*Then (.*)");
            if (then.Success)
            {
                code.AppendLine($"        {Then}().{FeatureGenerator.ToMethod(then.Groups[1].Value)}(); // {line.Trim()}");
                continue;
            }
        }

        CloseBackground();
        CloseScenario();

        code.AppendLine("}");

        return code.ToString();

        void CloseScenario()
        {
            if (scenario != null)
            {
                if (generateAfter != null && generateAfter(scenario))
                {
                    code.AppendLine();
                    code.AppendLine($"        {Moreover}().After{scenario.Method}();");
                }

                code.AppendLine("    }");
                code.AppendLine();

                code.AppendLine();
            }

            scenario = null;
        }

        void CloseBackground()
        {
            if (backgroundStarted != null)
            {
                code.AppendLine("    }");
                code.AppendLine();
            }

            backgroundStarted = null;
        }

        bool ResetInclude()
        {
            if (include != null)
                return false;

            if (exclude != null)
                return true;

            return true;
        }
    }

    private static string[] ReadAllLinesFrom(string gherkinPath)
    {
        if (File.Exists(gherkinPath))
            return File.ReadAllLines(gherkinPath);

        var gherkins = new[]
        {
            $"Feature: {Path.GetFileNameWithoutExtension(gherkinPath)}",
            "",
            "# TODO: Provide scenarios here. Check the sample down here.",
            "",
            "#  Scenario: There can be only one",
            "#    Given there are 3 ninjas",
            "#    And there are more than one ninja alive",
            "#    When Two ninjas meet, they will fight",
            "#    Then one ninja dies (but not me)",
            "#    And there is one ninja less alive",
        };

        using var stream = File.CreateText(gherkinPath);
        foreach (string line in gherkins)
        {
            stream.WriteLine(line);
        }

        stream.Close();

        return gherkins;
    }

    internal static string ToMethod(string sentence)
    {
        sentence = Regex.Replace(sentence, "[^A-Za-z0-9_]", " ");
        var parts = sentence.Split(" ");
        var m = string.Concat(parts.Where(p => !string.IsNullOrEmpty(p))
                                   .Select(p => p.Substring(0, 1)
                                                 .ToUpperInvariant() + p.Substring(1)));
        return m;
    }

    // private static string[]? ReadTagsFrom(string line)
    // {
    //     if (line.TrimStart().StartsWith("@") == false)
    //         return null;
    //
    //     return Regex.Match(line, "\\@\\w+")
    //                 .Groups.Values.Select(g => g.Value)
    //                 .ToArray();
    // }

    // private static bool StartsWithAny(this string line, params string[] starts)
    // {
    //     foreach (string start in starts)
    //     {
    //         if (line.StartsWith(start, StringComparison.InvariantCultureIgnoreCase))
    //             return true;
    //     }
    //
    //     return false;
    // }
}