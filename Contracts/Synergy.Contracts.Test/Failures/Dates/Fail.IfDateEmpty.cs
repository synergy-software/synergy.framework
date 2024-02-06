using System;
using Xunit;

namespace Synergy.Contracts.Test.Failures.Dates;

public class IfDateEmptyTest
{
    [Fact]
    public void IfDateEmptyWithName()
    {
        // ARRANGE
        DateTime minDate = DateTime.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfEmpty(minDate, nameof(minDate))
        );

        // ASSERT
        Assert.Equal("'minDate' is empty = 01/01/0001 00:00:00", exception.Message);
    }

    [Fact]
    public void IfDateEmptyWithCallerArgumentExpression()
    {
        // ARRANGE
        DateTime minDate = DateTime.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfEmpty(minDate)
        );

        // ASSERT
        Assert.Equal("'minDate' is empty = 01/01/0001 00:00:00", exception.Message);
    }
        
    [Fact]
    public void IfDateEmptySuccess()
    {
        // ACT
        Fail.IfEmpty(DateTime.Today, nameof(DateTime.Today));
        Fail.IfEmpty(DateTime.Today);
    }
}