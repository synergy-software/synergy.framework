using Xunit;

namespace Synergy.Contracts.Test.Failures.DatesOffset;

public class FailIfNotDateTimeOffsetTest
{
    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDatesWithTime), MemberType = typeof(DateTimeOffsetTestData))]
    public void FailIfNotDate(System.DateTimeOffset dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => dateTime.FailIfNotDate("dt")
        );

        // ASSERT
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDatesWithTime), MemberType = typeof(DateTimeOffsetTestData))]
    public void FailIfNotDateCallerArgumentExpression(System.DateTimeOffset dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => dateTime.FailIfNotDate()
        );

        // ASSERT
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDates), MemberType = typeof(DateTimeOffsetTestData))]
    public void FailIfNotDateSuccess(System.DateTimeOffset date)
    {
        // ACT
        System.DateTimeOffset returned = date.FailIfNotDate();

        // ASSERT
        Assert.Equal(date, returned);
    }
}

