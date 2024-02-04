using Xunit;

namespace Synergy.Contracts.Test.Failures.Boolean;

public class IfTrueTest
{
    [Fact]
    public void IfTrueWithMessage()
    {
        // ARRANGE
        var someTrueValue = true;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfTrue(someTrueValue, Violation.Of("this should be false {0}", 1))
        );

        // ASSERT
        Assert.Equal("this should be false 1", exception.Message);
    }
        
    [Fact]
    public void IfTrueSuccess()
    {
        // ARRANGE
        var someFalseValue = false;

        // ACT
        Fail.IfTrue(someFalseValue, Violation.Of("this should be false"));
    }
}