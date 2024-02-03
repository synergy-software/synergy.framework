using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        /// <summary>
        ///     Throws exception when the checked DateTime contains more than just a date - when it contains hours, minutes or
        ///     seconds fraction.
        ///     <para>
        ///         REMARKS: You can pass the <see langword="null" /> and it will not fail as there is nothing to check against
        ///         being a midnight time.
        ///     </para>
        /// </summary>
        /// <param name="date">Nullable DateTime to check.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <example>
        ///     <code>
        /// public List&lt;Contractor&gt; GetContractorsAged(DateTime minDate, DateTime? maxDate)
        /// {
        ///     Fail.IfNotDate(minDate, nameof(minDate));
        ///     Fail.IfNotDate(maxDate, nameof(maxDate));
        /// 
        ///     // WARN: Below is sample code with no sense at all
        ///     return new List&lt;Contractor&gt;(0);
        /// }
        /// </code>
        /// </example>
        [AssertionMethod]
        public static void IfNotDate(
            DateTime? date,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("date")] string? name = null
#else
            string name
#endif
        )
        {
            if (date == null)
                return;

            Fail.IfNotDate(date, Violation.WhenDateTimeIsNotDate(name, date.Value));
        }

        /// <summary>
        ///     Throws exception when the checked DateTime contains more than just a date - when it contains hours, minutes or
        ///     seconds fraction.
        ///     <para>
        ///         REMARKS: You can pass the <see langword="null" /> and it will not fail as there is nothing to check against
        ///         being a midnight time.
        ///     </para>
        /// </summary>
        /// <param name="date">Nullable DateTime to check.</param>
        /// <param name="message">
        ///     Message that will be passed to <see cref="DesignByContractViolationException" /> when the check
        ///     fails.
        /// </param>
        /// <example>
        ///     <code>
        /// public List&lt;Contractor&gt; GetContractorsAged(DateTime minDate, DateTime? maxDate)
        /// {
        ///     Fail.IfNotDate(minDate, "minDate must be a midnight");
        ///     Fail.IfNotDate(maxDate, "maxDate must be a midnight");
        /// 
        ///     // WARN: Below is sample code with no sense at all
        ///     return new List&lt;Contractor&gt;(0);
        /// }
        /// </code>
        /// </example>
        [AssertionMethod]
        public static void IfNotDate([CanBeNull] DateTime? date, Violation message)
        {
            if (date == null)
                return;

            DateTime dateTime = date.Value;
            Fail.IfNotEqual(dateTime.Date, dateTime, message);
        }

        /// <summary>
        ///     Throws exception when the checked DateTime contains more than just a date - when it contains hours, minutes or
        ///     seconds fraction.
        ///     <para>
        ///         REMARKS: You can pass the <see langword="null" /> and it will not fail as there is nothing to check against
        ///         being a midnight time.
        ///     </para>
        /// </summary>
        /// <param name="date">Nullable DateTime to check.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <returns></returns>
        [CanBeNull]
        [AssertionMethod]
        public static DateTime? FailIfNotDate(
            [CanBeNull] this DateTime? date,
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
        ///     Throws exception when the checked DateTime contains more than just a date - when it contains hours, minutes or
        ///     seconds fraction.
        ///     <para>
        ///         REMARKS: You can pass the <see langword="null" /> and it will not fail as there is nothing to check against
        ///         being a midnight time.
        ///     </para>
        /// </summary>
        /// <param name="date">Nullable DateTime to check.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <returns></returns>
        [AssertionMethod]
        public static DateTime FailIfNotDate(
            this DateTime date,
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
        /// Checks whether specified DateTime is empty - is equal to DateTime.MinValue.
        /// If it is - contract violation exception is thrown.
        /// </summary>
        /// <param name="value">DateTime to check</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static void IfEmpty(
            DateTime value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")]
            string? name = null
#else
            string name
#endif
        )
        {
            if (value == DateTime.MinValue)
                throw Fail.Because(Violation.WhenDateTimeIsEmpty(name, value));
        }

        /// <summary>
        /// Checks whether specified DateTime is empty - is equal to DateTime.MinValue.
        /// If it is - contract violation exception is thrown.
        /// </summary>
        /// <param name="value">DateTime to check</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static DateTime FailIfEmpty(this DateTime value, [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.IfEmpty(value, name);
            return value;
        }
    }
}