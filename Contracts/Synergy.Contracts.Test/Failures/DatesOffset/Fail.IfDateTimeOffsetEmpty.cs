using Xunit;

namespace Synergy.Contracts.Test.Failures.DatesOffset;

public class IfDateTimeOffsetEmptyTest
{
    [Fact]
    public void IfDateEmptyWithName()
    {
        // ARRANGE
        System.DateTimeOffset minDate = System.DateTimeOffset.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfEmpty(minDate, "minDateName")
        );

        // ASSERT
        Assert.Equal("'minDateName' is empty = 01/01/0001 00:00:00 +00:00", exception.Message);
    }

    [Fact]
    public void IfDateEmptyWithCallerArgumentExpression()
    {
        // ARRANGE
        System.DateTimeOffset minDate = System.DateTimeOffset.MinValue;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfEmpty(minDate)
        );

        // ASSERT
        Assert.Equal("'minDate' is empty = 01/01/0001 00:00:00 +00:00", exception.Message);
    }

    [Fact]
    public void IfDateEmptySuccess()
    {
        // ACT
        Fail.IfEmpty(System.DateTimeOffset.UtcNow.Date, nameof(System.DateTimeOffset.UtcNow));
        Fail.IfEmpty(System.DateTimeOffset.UtcNow.Date);
    }
}

