using Xunit;
using Assert = Xunit.Assert;

namespace Synergy.Contracts.Test.Failures.Boolean;

public class IfFalseTest
{
    [Fact]
    public void IfFalseWithMessage()
    {
        // ARRANGE
        bool someFalseValue = false;
    
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfFalse(someFalseValue, Violation.Of("this should be true {0}", 1))
        );
    
        // ASSERT
        Assert.Equal("this should be true 1", exception.Message);
    }

    [Fact]
    public void IfFalseSuccess()
    {
        // ARRANGE
        var someTrueValue = true;

        // ACT
        Fail.IfFalse(someTrueValue, Violation.Of("this should be true"));
    }
}