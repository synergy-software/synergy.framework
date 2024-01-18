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
            if (token.Type == Comment.Keyword)
                continue;

            if (token.Type == Tag.Keyword)
            {
                tags.Add(token.Value);
                continue;
            }

            if (token.Type.In(Scenario.Keywords))
            {
                var scenario = GherkinParser.ParseScenario(token, tags, stack, currentRule);
                feature.Scenarios.Add(scenario);
                tags = new List<string>();
                continue;
            }

            if (token.Type == Background.Keyword)
            {
                var background = GherkinParser.ParseBackground(token, stack);
                
                if (currentRule != null)
                    currentRule = currentRule with { Background = background };
                else
                    feature = feature with { Background = background };
                
                tags = new List<string>();
                continue;
            }

            if (token.Type == Rule.Keyword)
            {
                currentRule = GherkinParser.ParseRule(token);
                tags = new List<string>();
                continue;
            }
            
            if (token.Type.In(ScenarioOutline.Keywords))
            {
                var scenario = GherkinParser.ParseScenarioOutline(token, tags, stack, currentRule);
                feature.Scenarios.Add(scenario);
                tags = new List<string>();
                continue;
            }
            
            throw new Exception($"Unsupported token at line {token.Line.Number}: {token.Line.Text.Trim()}");
        }

        return feature;
    }
    
    private static Feature ParseFeature(Stack<GherkinToken> stack)
    {
        var feature = new Feature("", new List<string>(), null, new List<Scenario>(), new Line(0, ""));
        while (stack.Any())
        {
            var token = stack.Pop();
            if (token.Type == Comment.Keyword)
                continue;

            if (token.Type == Tag.Keyword)
            {
                feature.Tags.Add(token.Value);
                continue;
            }

            if (token.Type == Feature.Keyword)
            {
                feature = feature with { Title = token.Value, Line = token.Line };
                break;
            }

            // TODO: Marcin Celej [from: Marcin Celej on: 18-01-2024]: Parse description lines after Feature: line 
        }

        return feature;
    }
    
    private static Background ParseBackground(GherkinToken token, Stack<GherkinToken> stack)
    {
        var background = new Background(new List<Step>(), token.Line);
        while (stack.Any())
        {
            var stepToken = stack.Pop();
            if (stepToken.Type == Comment.Keyword)
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
    
    
    private static Scenario ParseScenario(GherkinToken token, List<string> tags, Stack<GherkinToken> stack, Rule? rule)
    {
        var scenario = new Scenario(token.Value, tags, new List<Step>(), rule, token.Line);
        while (stack.Any())
        {
            var stepToken = stack.Pop();
            if (stepToken.Type == Comment.Keyword)
                continue;

            if (stepToken.Type.In(Step.Keywords))
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
    
    
    private static Scenario ParseScenarioOutline(GherkinToken token, List<string> tags, Stack<GherkinToken> stack, Rule? rule)
    {
        var scenario = new ScenarioOutline(token.Value, tags, new List<Step>(), rule, null!, token.Line);
        while (stack.Any())
        {
            var stepToken = stack.Pop();
            if (stepToken.Type == Comment.Keyword)
                continue;

            if (stepToken.Type.In(Step.Keywords))
            {
                var step = new Step(stepToken.Type, stepToken.Value, stepToken.Line);
                scenario.Steps.Add(step);
                continue;
            }

            if (stepToken.Type == Examples.Keyword)
            {
                var examples = ParseExamples(stepToken, stack);
                scenario = scenario with { Examples = examples };
                continue;
            }
            
            stack.Push(stepToken);
            break;
        }
        
        if (scenario.Examples == null)
            throw new Exception($"Scenario Outline must have Examples section. Line: {token.Line.Number}: {token.Line.Text.Trim()}");

        return scenario;
    }

    private static Examples ParseExamples(GherkinToken token, Stack<GherkinToken> stack)
    {
        var headerToken = stack.Pop();
        var header = ParseRow(headerToken);
        var examples = new Examples(header, new List<Examples.Row>(), token.Line);

        while (stack.Any())
        {
            var stepToken = stack.Pop();
            if (stepToken.Type == Comment.Keyword)
                continue;

            if (stepToken.Type == Examples.Row.Keyword)
            {
                var row = ParseRow(stepToken);
                examples.Rows.Add(row);
                continue;
            }
            
            stack.Push(stepToken);
            break;
        }
        
        return examples;

        Examples.Row ParseRow(GherkinToken theToken)
        {
            List<string> values = theToken.Value.Trim('|').Split('|').Select(x => x.Trim()).ToList();
            return new Examples.Row(values, theToken.Line);
        }
    }
}