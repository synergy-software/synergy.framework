using System;
using System.Collections.Generic;

namespace Synergy.Contracts.Test.Failures.DatesOffset;

public class DateTimeOffsetTestData
{
    public static IEnumerable<object?[]> GetDates()
    {
        yield return new object?[] { null };
        //yield return new object[] { DateTimeOffset.UtcNow.Date };
        yield return new object[] { DateTimeOffset.MinValue };
        //yield return new object[] { DateTimeOffset.MaxValue.Date };
        yield return new object[] { new DateTimeOffset(2019, 03, 26, 0, 0, 0, TimeSpan.FromHours(2)) };
    }

    public static IEnumerable<object[]> GetDatesWithTime()
    {
        yield return new object[] { DateTimeOffset.MaxValue };
        yield return new object[] { new DateTimeOffset(2019, 03, 26, 0, 0, 0, TimeSpan.FromHours(2)).AddMilliseconds(1) };
    }
}

