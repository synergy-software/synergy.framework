using Xunit;

namespace Synergy.Contracts.Test.Failures.DatesOffset;

public class IfNotDateTimeOffsetTest
{
    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDatesWithTime), MemberType = typeof(DateTimeOffsetTestData))]
    public void IfNotDateWithMessage(System.DateTimeOffset dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfNotDate(dateTime, Violation.Of("date should have no hour nor second"))
        );

        // ASSERT
        Assert.Equal("date should have no hour nor second", exception.Message);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDatesWithTime), MemberType = typeof(DateTimeOffsetTestData))]
    public void IfNotDateWithName(System.DateTimeOffset dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfNotDate(dateTime, "dateTimeName")
        );

        // ASSERT
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDatesWithTime), MemberType = typeof(DateTimeOffsetTestData))]
    public void IfNotDateWithCallerArgumentExpression(System.DateTimeOffset dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfNotDate(dateTime)
        );

        // ASSERT
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDates), MemberType = typeof(DateTimeOffsetTestData))]
    public void IfNotDateSuccessWithMessage(System.DateTimeOffset? date)
    {
        // ACT
        Fail.IfNotDate(date, Violation.Of("date should have no hour nor second"));
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetTestData.GetDates), MemberType = typeof(DateTimeOffsetTestData))]
    public void IfNotDateSuccessWithName(System.DateTimeOffset? date)
    {
        // ACT
        Fail.IfNotDate(date, nameof(date));
        Fail.IfNotDate(date);
    }
}

