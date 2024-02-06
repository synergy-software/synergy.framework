using System;
using Synergy.Contracts.Samples;
using Xunit;

namespace Synergy.Contracts.Test.Failures.Dates;

public class IfNotDateTest
{
    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDatesWithTime), MemberType = typeof(DateTimeTestData))]
    public void IfNotDateWithMessage(DateTime dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfNotDate(dateTime, Violation.Of("date should have no hour nor second"))
        );

        // ASSERT
        Assert.Equal("date should have no hour nor second", exception.Message);
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDatesWithTime), MemberType = typeof(DateTimeTestData))]
    public void IfNotMidnightWithName(DateTime dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfNotDate(dateTime, nameof(dateTime))
        );

        // ASSERT
        Assert.NotNull(exception);
        //Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDatesWithTime), MemberType = typeof(DateTimeTestData))]
    public void IfNotMidnightWithNameCallerArgumentExpression(DateTime dateTime)
    {
        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => Fail.IfNotDate(dateTime)
        );

        // ASSERT
        Assert.NotNull(exception);
        //Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDates), MemberType = typeof(DateTimeTestData))]
    public void IfNotMidnightSuccessWithMessage(DateTime? date)
    {
        // ACT
        Fail.IfNotDate(date, Violation.Of("date should have no hour nor second"));
    }

    [Theory]
    [MemberData(nameof(DateTimeTestData.GetDates), MemberType = typeof(DateTimeTestData))]
    public void IfNotMidnightSuccessWithName(DateTime? date)
    {
        // ACT
        Fail.IfNotDate(date, nameof(date));
        Fail.IfNotDate(date);
    }

    [Fact]
    public void IfNotMidnightSample()
    {
        // ARRANGE
        IContractorRepository contractorRepository = new ContractorRepository();
        DateTime ratherNotMidnight = DateTime.Now;

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => contractorRepository.GetContractorsAged(ratherNotMidnight, null));

        // ASSERT
        Assert.NotNull(exception);
        Assert.Equal("minDate must be a midnight", exception.Message);
    }
}