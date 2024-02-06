using System;
using Xunit;

namespace Synergy.Contracts.Test.Failures.Dates;

public class FailIfEmptyTest
{
    [Fact]
    public void FailIfDateEmpty()
    {
        // ARRANGE
        DateTime minDate = DateTime.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => minDate.FailIfEmpty(nameof(minDate))
        );

        // ASSERT
        Assert.Equal("'minDate' is empty = 01/01/0001 00:00:00", exception.Message);
    }
        
    [Fact]
    public void FailIfDateEmptyCallerArgumentExpression()
    {
        // ARRANGE
        DateTime minDate = DateTime.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => minDate.FailIfEmpty()
        );

        // ASSERT
        Assert.Equal("'minDate' is empty = 01/01/0001 00:00:00", exception.Message);
    }

    [Fact]
    public void FailIfDateEmptySuccess()
    {
        // ACT
        // ReSharper disable once UnusedVariable
        var date1 = DateTime.Today.FailIfEmpty(nameof(DateTime.Today));
        // ReSharper disable once UnusedVariable
        var date2 = DateTime.Today.FailIfEmpty();
    }

}