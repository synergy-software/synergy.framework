﻿using System.Text;
using System.Text.RegularExpressions;
using Synergy.Behaviours.Testing.Gherkin;
using Feature = Synergy.Behaviours.Testing.Gherkin.Feature;

namespace Synergy.Behaviours.Testing.Generator;

internal class XUnitFeatureGenerator
{
    private readonly Func<Scenario, bool> include;
    private readonly Func<Scenario, bool> generateAfter;
    private readonly bool currentScenario = true;

    public XUnitFeatureGenerator(
        Func<Scenario, bool>? include,
        Func<Scenario, bool>? generateAfter
    )
    {
        this.include = include ?? (_ => true);
        this.generateAfter = generateAfter ?? (_ => false);
    }

    public StringBuilder Generate(
        Feature feature,
        object featureClass
    )
    {
        StringBuilder code = new StringBuilder();
        code.AppendLine("// <auto-generated />");
        code.AppendLine("using System.CodeDom.Compiler;");
        code.AppendLine();
        code.AppendLine($"namespace {featureClass.GetType().Namespace};");
        code.AppendLine();
        this.GenerateTraits(code, feature.Tags);
        code.AppendLine(
            $"[GeneratedCode(\"{typeof(FeatureGenerator).Assembly.GetName().Name}\", " +
            $"\"{typeof(FeatureGenerator).Assembly.GetName().Version?.ToString()}\")]"
        );
        code.AppendLine($"partial class {featureClass.GetType().Name} // {feature.Line.Text.Trim()}");
        code.AppendLine("{");
        var backgroundMethod = this.GenerateBackground(code, feature);
        Rule? currentRule = null;

        foreach (var scenario in feature.Scenarios.Where(this.include))
        {
            if (currentRule != scenario.Rule)
            {
                currentRule = scenario.Rule;
                code.AppendLine($"    // {currentRule!.Line.Text.Trim()}");
                code.AppendLine();

                if (currentRule.Background != null)
                {
                    backgroundMethod = this.GenerateBackground(code, currentRule);
                }
            }

            this.Generate(code, scenario, backgroundMethod);
        }

        if (this.currentScenario)
            code.AppendLine("    partial void CurrentScenario(params string[] scenario);");

        code.AppendLine("}");

        return code;
    }

    private void GenerateTraits(StringBuilder code, List<string> tags, string indent = "")
    {
        foreach (string tag in tags)
        {
            code.AppendLine($"{indent}[Xunit.Trait(\"Category\", \"{tag}\")] // @{tag}");
        }
    }

    private string? GenerateBackground(StringBuilder code, Feature feature)
    {
        Background? background = feature.Background;
        if (background == null)
            return null;

        var backgroundMethod = Sentence.ToMethod(feature.Title) + "Background";
        this.GenerateBackgroundMethod(code, backgroundMethod, background);
        return backgroundMethod;
    }

    private string GenerateBackground(StringBuilder code, Rule rule)
    {
        Background background = rule.Background ?? throw new ArgumentNullException(nameof(rule.Background));
        var backgroundMethod = Sentence.ToMethod(rule.Title) + "Background";
        this.GenerateBackgroundMethod(code, backgroundMethod, background);
        return backgroundMethod;
    }
    
    private void GenerateBackgroundMethod(StringBuilder code, string backgroundMethod, Background background)
    {
        code.AppendLine($"    private void {backgroundMethod}() // {background.Line.Text.Trim()}");
        code.AppendLine("    {");
        this.GenerateSteps(code, background.Steps);
        code.AppendLine("    }");
        code.AppendLine();
    }

    private void Generate(StringBuilder code, Scenario scenario, string? backgroundMethod)
    {
        this.GenerateTraits(code, scenario.Tags, "    ");
        string scenarioOriginalTitle = scenario.Line.Text.Trim();
        string methodName = Sentence.ToMethod(scenario.Title);
        var arguments = "";
        string displayName = scenarioOriginalTitle.Replace("\"", "\\\"");
        if (scenario is ScenarioOutline)
        {
            arguments = GenerateScenarioOutlineAsXunitTheory();
        }
        else
        {
            code.AppendLine($"    [Xunit.Fact(DisplayName = \"{displayName}\")]");
        }
        code.AppendLine($"    public void {methodName}({arguments}) // {scenarioOriginalTitle}");
        code.AppendLine("    {");

        if (this.currentScenario)
        {
            code.AppendLine($"       CurrentScenario(");
            code.AppendLine($"           \"{string.Join($"\",{Environment.NewLine}           \"", scenario.Lines.Select(line => line.Replace("\"", "\\\"")))}\"");
            code.AppendLine($"       );");
            code.AppendLine();
        }

        if (backgroundMethod != null)
            code.AppendLine($"       Background().{backgroundMethod}();");
        else
            code.AppendLine($"       Background();");
        
        code.AppendLine();
        this.GenerateSteps(code, scenario.Steps);

        if (this.generateAfter(scenario))
        {
            code.AppendLine();
            code.AppendLine($"        Moreover().After{methodName}();");
        }

        code.AppendLine("    }");
        code.AppendLine();

        string GenerateScenarioOutlineAsXunitTheory()
        {
            Examples examples = ((ScenarioOutline) scenario).Examples;
            arguments = "string " + string.Join(", string ", examples.Header.Values.Select(argument => Sentence.ToArgument(argument)));
            code.AppendLine($"    [Xunit.Theory(DisplayName = \"{displayName}\")]");
            
            foreach (var row in examples.Rows)
            {
                code.AppendLine($"    [Xunit.InlineData(\"{string.Join("\", \"", row.Values)}\")]");
            }

            return arguments;
        }
    }
    
    private void GenerateSteps(StringBuilder code, List<Step> steps)
    {
        var argumentsRegex = new Regex("<(.*?)>");

        var max = steps.Max(step => MethodCall(step).Length) + 2;
        foreach (var step in steps)
        {
            // TODO: Marcin Celej [from: Marcin Celej on: 18-01-2024]: Consider Introducing inherited Step wit arguments (in outline only)

            string methodCall = MethodCall(step);
            var spaces = new string(' ', max - methodCall.Length);
            code.AppendLine($"       {methodCall};{spaces}// {step.Line.Text.Trim()}");
        }

        string MethodCall(Step theStep)
        {
            string stepType = GetStepType(theStep);
            string stepText = theStep.Text;
            string methodName = Sentence.ToMethod(argumentsRegex.Replace(stepText, ""));
            var arguments = argumentsRegex.Matches(stepText).Select(match => Sentence.ToArgument(match.Groups[1].Value)).ToArray();
            return $"{stepType}().{methodName}({string.Join(", ", arguments)})";
        }
        
        string GetStepType(Step step)
        {
            var type = step.Type;
            if (step.Type == "*")
                type = "And";

            return new string(' ', 5 - type.Length) + type;
        }
    }
}