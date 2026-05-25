using Xunit;

namespace Synergy.Contracts.Test.Failures.DatesOffset;

public class FailIfNotNullableDateTimeOffsetTest
{
    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDatesWithTime), MemberType = typeof(DateTimeOffsetTestData))]
    public void FailIfNotNullableDate(System.DateTimeOffset? dateTime)
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
    public void FailIfNotNullableDateCallerArgumentExpression(System.DateTimeOffset? dateTime)
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
    public void FailIfNotNullableDateSuccess(System.DateTimeOffset? date)
    {
        // ACT
        System.DateTimeOffset? returned = date.FailIfNotDate("dt");

        // ASSERT
        Assert.Equal(date, returned);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDates), MemberType = typeof(DateTimeOffsetTestData))]
    public void FailIfNotNullableDateSuccessCallerArgumentExpression(System.DateTimeOffset? date)
    {
        // ACT
        System.DateTimeOffset? returned = date.FailIfNotDate();

        // ASSERT
        Assert.Equal(date, returned);
    }
}

