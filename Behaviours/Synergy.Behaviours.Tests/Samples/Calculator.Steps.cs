using Synergy.Behaviours.Testing;

namespace Synergy.Behaviours.Tests.Samples;

[UsesVerify]
public partial class CalculatorFeature : Feature<CalculatorFeature>
{
    [Fact]
    public async Task GenerateFeature()
    {
        var code = this.Generate(
            from: "Calculator.feature",
            include: new[] { "@Add" },
            //exclude: new[] { "@Divide" },
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
                .GetAwaiter().GetResult();
    }

    private void TwoNumbers()
    {
    }
}