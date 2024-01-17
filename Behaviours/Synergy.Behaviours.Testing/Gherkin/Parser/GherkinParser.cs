using Synergy.Behaviours.Testing.Gherkin.Tokenizer;
using Synergy.Catalogue;

namespace Synergy.Behaviours.Testing.Gherkin.Parser;

internal static class GherkinParser
{
    public static Feature Parse(string[] lines)
    {
        var tokens = GherkinTokenizer.Tokenize(lines);
        Stack<GherkinToken> stack = new Stack<GherkinToken>(tokens.Reverse());
        var feature = GherkinParser.ParseFeature(stack);
        Rule? currentRule = null;
        var tags = new List<string>();
        
        while (stack.Any())
        {
            var token = stack.Pop();
            if (token.Type == "#")
                continue;

            if (token.Type == "@")
            {
                tags.Add(token.Value);
                continue;
            }

            if (token.Type == "Scenario" || token.Type == "Example")
            {
                var scenario = GherkinParser.ParseScenario(token, tags, stack, currentRule);
                feature.Scenarios.Add(scenario);
                tags = new List<string>();
                continue;
            }

            if (token.Type == "Background")
            {
                var background = GherkinParser.ParseBackground(token, stack);
                
                if (currentRule != null)
                    currentRule = currentRule with { Background = background };
                else
                    feature = feature with { Background = background };
                
                tags = new List<string>();
                continue;
            }

            if (token.Type == "Rule")
            {
                currentRule = GherkinParser.ParseRule(token);
                tags = new List<string>();
                continue;
            }
            
            throw new Exception("Unsupported token at line " + token.Line.Number + ": " + token.Line.Text.Trim());
        }

        return feature;
    }
    
    private static Feature ParseFeature(Stack<GherkinToken> stack)
    {
        var feature = new Feature("", new List<string>(), null, new List<Scenario>(), new Line(0, ""));
        while (stack.Any())
        {
            var token = stack.Pop();
            if (token.Type == "#")
                continue;

            if (token.Type == "@")
            {
                feature.Tags.Add(token.Value);
                continue;
            }

            if (token.Type == "Feature")
            {
                feature = feature with { Title = token.Value, Line = token.Line };
                break;
            }
        }

        return feature;
    }

    private static Scenario ParseScenario(GherkinToken token, List<string> tags, Stack<GherkinToken> stack, Rule? rule)
    {
        var scenario = new Scenario(token.Value, tags, new List<Step>(), rule, token.Line);
        while (stack.Any())
        {
            var stepToken = stack.Pop();
            if (stepToken.Type == "#")
                continue;

            if (stepToken.Type.In("Given", "When", "Then", "And", "But", "*"))
            {
                var step = new Step(stepToken.Type, stepToken.Value, stepToken.Line);
                scenario.Steps.Add(step);
                continue;
            }

            stack.Push(stepToken);
            break;
        }

        return scenario;
    }
    
    private static Background ParseBackground(GherkinToken token, Stack<GherkinToken> stack)
    {
        var background = new Background(new List<Step>(), token.Line);
        while (stack.Any())
        {
            var stepToken = stack.Pop();
            if (stepToken.Type == "#")
                continue;

            if (stepToken.Type.In("Given", "And", "But", "*"))
            {
                var step = new Step(stepToken.Type, stepToken.Value, stepToken.Line);
                background.Steps.Add(step);
                continue;
            }

            stack.Push(stepToken);
            break;
        }

        return background;
    }

    private static Rule ParseRule(GherkinToken token) 
        => new(token.Value, null, token.Line);
}