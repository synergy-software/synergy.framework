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

        await Verifier.Verify(code, "cs").UseFileName("Calculator.Feature");
    }

    private int _firstNumber;

    private CalculatorFeature TheFirstNumberIs50()
    {
        this._firstNumber = 50;
        return this;
    }

    private int _secondNumber;

    private CalculatorFeature TheSecondNumberIs70()
    {
        this._secondNumber = 70;
        return this;
    }

    private int _result;
    private CalculatorFeature TheTwoNumbersAreAdded()
    {
        var calculator = new Calculator { FirstNumber = this._firstNumber, SecondNumber = this._secondNumber };
        this._result = calculator.Add();
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

    private CalculatorFeature TwoNumbers()
    {
        return this;
    }
}