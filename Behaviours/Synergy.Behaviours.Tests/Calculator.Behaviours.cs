using Synergy.Behaviours.Testing;
using Xunit;

namespace Synergy.Behaviours.Tests;

public partial class CalculatorFeature : Feature<CalculatorFeature>
{
    [Fact]
    public void GenerateFeature()
        => this.Generate(
            from: "Calculator.feature",
            to: "Calculator.Feature.cs"
        );

    private int _firstNumber;

    private CalculatorFeature TheFirstNumberIs50()
    {
        this._firstNumber = 50;
        return this;
    }

    private int _secondNumber;
    private int _result;

    private CalculatorFeature TheSecondNumberIs70()
    {
        this._secondNumber = 70;
        return this;
    }

    private CalculatorFeature TheTwoNumbersAreAdded()
    {
        var calculator = new Calculator { FirstNumber = this._firstNumber, SecondNumber = this._secondNumber };
        _result = calculator.Add();
        return this;
    }

    private CalculatorFeature TheResultShouldBe120()
    {
        Assert.Equal(120, this._result);
        return this;
    }

    private CalculatorFeature VerifyAddTwoNumbers()
    {
        return this;
    }
}