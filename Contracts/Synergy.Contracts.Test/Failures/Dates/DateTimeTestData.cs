using System;
using System.Collections.Generic;

namespace Synergy.Contracts.Test.Failures.Dates;

public class DateTimeTestData
{
    public static IEnumerable<object?[]> GetDates()
    {
        yield return new object?[] { null };
        yield return new object[] { DateTime.Today };
        yield return new object[] { DateTime.MinValue };
        yield return new object[] { DateTime.MaxValue.Date };
        yield return new object[] { new DateTime(2019, 03, 26) };
    }

    public static IEnumerable<object[]> GetDatesWithTime()
    {
        yield return new object[] { DateTime.MaxValue };
        yield return new object[] { new DateTime(2019, 03, 26).AddMilliseconds(1) };
    }
}