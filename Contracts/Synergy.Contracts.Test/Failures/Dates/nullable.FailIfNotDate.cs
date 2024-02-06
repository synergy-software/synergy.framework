using System;
using Xunit;

namespace Synergy.Contracts.Test.Failures.Dates;

public class FailIfNotNullableDateTest
{
    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDatesWithTime), MemberType = typeof(DateTimeTestData))]
    public void FailIfNotNullableDate(DateTime? dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => dateTime.FailIfNotDate(nameof(dateTime))
        );

        // ASSERT
        Assert.NotNull(exception);
        //Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDatesWithTime), MemberType = typeof(DateTimeTestData))]
    public void FailIfNotNullableDateCallerArgumentExpression(DateTime? dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => dateTime.FailIfNotDate()
        );

        // ASSERT
        Assert.NotNull(exception);
        //Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDates), MemberType = typeof(DateTimeTestData))]
    public void FailIfNotNullableDateSuccess(DateTime? date)
    {
        // ACT
        DateTime? returned = date.FailIfNotDate(nameof(date));

        // ASSERT
        Assert.Equal(date, returned);
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDates), MemberType = typeof(DateTimeTestData))]
    public void FailIfNotNullableDateSuccessCallerArgumentExpression(DateTime? date)
    {
        // ACT
        DateTime? returned = date.FailIfNotDate();

        // ASSERT
        Assert.Equal(date, returned);
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDates), MemberType = typeof(DateTimeTestData))]
    public void FailIfNotDateSuccess(DateTime? date)
    {
        // ACT
        DateTime? returned = date.FailIfNotDate(nameof(date));

        // ASSERT
        Assert.Equal(date, returned);
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDates), MemberType = typeof(DateTimeTestData))]
    public void FailIfNotDateSuccessCallerArgumentExpression(DateTime? date)
    {
        // ACT
        DateTime? returned = date.FailIfNotDate();

        // ASSERT
        Assert.Equal(date, returned);
    }
}