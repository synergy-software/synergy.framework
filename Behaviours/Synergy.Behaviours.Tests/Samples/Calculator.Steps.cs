using System.Runtime.CompilerServices;
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
            include: new[] { "@Add" }
            //exclude: new[] { "@Divide" }
        );

        await Verifier
              .Verify(code, "cs")
              .UseFileName("Calculator.Feature")
              .AutoVerify();
    }

    private Calculator _calculator;
    private CalculatorFeature UserOpenedCalculator()
    {
        this._calculator = new Calculator();
        return this;
    }
    
    private CalculatorFeature TheFirstNumberIs50()
    {
        this._calculator.FirstNumber = 50;
        return this;
    }

    private CalculatorFeature TheSecondNumberIs70()
    {
        this._calculator.SecondNumber = 70;
        return this;
    }

    private int _result;
    private CalculatorFeature TheTwoNumbersAreAdded()
    {
        this._result = this._calculator.Add();
        return this;
    }

    private CalculatorFeature TheResultShouldBe120()
    {
        Assert.Equal(120, this._result);
        return this;
    }

    partial void AfterAddTwoNumbers()
    {
        Verifier.Verify(this._calculator)
                .UseDirectory("Snapshots")
                .GetAwaiter().GetResult();
    }

    private CalculatorFeature TwoNumbers()
    {
        return this;
    }
}