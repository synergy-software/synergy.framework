using Xunit;

namespace Synergy.Contracts.Test.Failures.DatesOffset;

public class FailIfDateTimeOffsetEmptyTest
{
    [Fact]
    public void FailIfDateEmpty()
    {
        // ARRANGE
        System.DateTimeOffset minDate = System.DateTimeOffset.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => minDate.FailIfEmpty("minD")
        );

        // ASSERT
        Assert.Equal("'minD' is empty = 01/01/0001 00:00:00 +00:00", exception.Message);
    }

    [Fact]
    public void FailIfDateEmptyCallerArgumentExpression()
    {
        // ARRANGE
        System.DateTimeOffset minDate = System.DateTimeOffset.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => minDate.FailIfEmpty()
        );

        // ASSERT
        Assert.Equal("'minDate' is empty = 01/01/0001 00:00:00 +00:00", exception.Message);
    }

    [Fact]
    public void FailIfDateEmptySuccess()
    {
        // ACT
        // ReSharper disable once UnusedVariable
        var date1 = System.DateTimeOffset.UtcNow.Date.FailIfEmpty(nameof(System.DateTimeOffset.UtcNow));
        // ReSharper disable once UnusedVariable
        var date2 = System.DateTimeOffset.UtcNow.Date.FailIfEmpty();
    }
}

