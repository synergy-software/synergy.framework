using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        /// <summary>
        ///     Throws exception when the checked DateTimeOffset contains more than just a date - when it contains
        ///     hours, minutes or seconds fraction.
        ///     <para>
        ///         REMARKS: You can pass the <see langword="null" /> and it will not fail as there is nothing to check
        ///         against being a midnight time.
        ///     </para>
        /// </summary>
        /// <param name="date">Nullable DateTimeOffset to check.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static void IfNotDate(
            DateTimeOffset? date,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("date")] string? name = null
#else
            string name
#endif
        )
        {
            if (date == null)
                return;

            Fail.IfNotDate(date, Violation.WhenDateTimeOffsetIsNotDate(name, date.Value));
        }

        /// <summary>
        ///     Throws exception when the checked DateTimeOffset contains more than just a date - when it contains
        ///     hours, minutes or seconds fraction.
        ///     <para>
        ///         REMARKS: You can pass the <see langword="null" /> and it will not fail as there is nothing to check
        ///         against being a midnight time.
        ///     </para>
        /// </summary>
        /// <param name="date">Nullable DateTimeOffset to check.</param>
        /// <param name="message">
        ///     Message that will be passed to <see cref="DesignByContractViolationException" /> when the check fails.
        /// </param>
        [AssertionMethod]
        public static void IfNotDate([CanBeNull] DateTimeOffset? date, Violation message)
        {
            if (date == null)
                return;

            DateTimeOffset dateTime = date.Value;
            Fail.IfNotEqual(TimeSpan.Zero, dateTime.TimeOfDay, message);
        }

        /// <summary>
        ///     Throws exception when the checked DateTimeOffset contains more than just a date - when it contains
        ///     hours, minutes or seconds fraction.
        /// </summary>
        /// <param name="date">Nullable DateTimeOffset to check.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <returns></returns>
        [CanBeNull]
        [AssertionMethod]
        public static DateTimeOffset? FailIfNotDate(
            [CanBeNull] this DateTimeOffset? date,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("date")]
            string? name = null
#else
            string name
#endif
        )
        {
            Fail.IfNotDate(date, name);

            return date;
        }

        /// <summary>
        ///     Throws exception when the checked DateTimeOffset contains more than just a date - when it contains
        ///     hours, minutes or seconds fraction.
        /// </summary>
        /// <param name="date">Nullable DateTimeOffset to check.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <returns></returns>
        [AssertionMethod]
        public static DateTimeOffset FailIfNotDate(
            this DateTimeOffset date,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("date")]
            string? name = null
#else
            string name
#endif
        )
        {
            Fail.IfNotDate(date, name);

            return date;
        }

        /// <summary>
        /// Checks whether specified DateTimeOffset is empty - is equal to DateTimeOffset.MinValue.
        /// If it is - contract violation exception is thrown.
        /// </summary>
        /// <param name="value">DateTimeOffset to check</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static void IfEmpty(
            DateTimeOffset value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")]
            string? name = null
#else
            string name
#endif
        )
        {
            if (value == DateTimeOffset.MinValue)
                throw Fail.Because(Violation.WhenDateTimeOffsetIsEmpty(name, value));
        }

        /// <summary>
        /// Checks whether specified DateTimeOffset is empty - is equal to DateTimeOffset.MinValue.
        /// If it is - contract violation exception is thrown.
        /// </summary>
        /// <param name="value">DateTimeOffset to check</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static DateTimeOffset FailIfEmpty(
            this DateTimeOffset value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")]
            string? name = null
#else
            string name
#endif
        )
        {
            Fail.IfEmpty(value, name);
            return value;
        }
    }
}

