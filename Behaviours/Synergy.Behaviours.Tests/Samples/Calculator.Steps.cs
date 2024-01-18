using Synergy.Behaviours.Testing;

namespace Synergy.Behaviours.Tests.Samples;

[UsesVerify]
public partial class CalculatorFeature : Feature<CalculatorFeature>
{
    [Fact(DisplayName = "!!!!")]
    public async Task GenerateFeature()
    {
        var code = this.Generate(
            from: "Calculator.feature",
            include: scenario => scenario.IsTagged("Add"),
            generateAfter: scenario => scenario.IsTagged("verify")
        );

        await Verifier
              .Verify(code, "cs")
              .UseFileName("Calculator.Feature")
              .AutoVerify();
    }

    private Calculator _calculator = null!;
    private void UserOpenedCalculator()
    {
        this._calculator = new Calculator();
    }
    
    private void TheFirstNumberIs50()
    {
        this._calculator.FirstNumber = 50;
    }

    private void TheSecondNumberIs70()
    {
        this._calculator.SecondNumber = 70;
    }

    private int _result;
    private string[]? _scenario;

    private void TheTwoNumbersAreAdded()
    {
        this._result = this._calculator.Add();
    }

    private void TheResultShouldBe120()
    {
        Assert.Equal(120, this._result);
    }

    private void AfterAddTwoNumbers()
    {
        Verifier.Verify(this._calculator)
                .UseDirectory("Snapshots")
                .AppendValue("verifies", this._scenario!)
                .GetAwaiter().GetResult();
    }

    private void TwoNumbers()
    {
    }

    partial void CurrentScenario(params string[] scenario)
    {
        _scenario = scenario;
    }

    private void TheFirstNumberIs(string first)
    {
        this._calculator.FirstNumber = Convert.ToInt32(first);
    }

    private void TheSecondNumberIs(string second)
    {
        this._calculator.SecondNumber = Convert.ToInt32(second);
    }

    private void TheResultShouldBe(string result)
    {
        Assert.Equal(Convert.ToInt32(result), this._result);
    }
}