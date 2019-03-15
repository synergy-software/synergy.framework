using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        // TODO:mace (from:mace @ 22-10-2016): variable.FailIfNotMidnight(nameof(variable))

        /// <summary>
        ///     Throws exception when the checked DateTime contains more than just a date - when it contains hours, minutes or
        ///     seconds fraction.
        ///     <para>
        ///         REMARKS: You can pass the <see langword="null" /> and it will not fail as there is nothing to check against
        ///         being a midnight time.
        ///     </para>
        /// </summary>
        /// <param name="value">Nullable DateTime to check.</param>
        /// <param name="message">
        ///     Message that will be passed to <see cref="DesignByContractViolationException" /> when the check
        ///     fails.
        /// </param>
        /// <example>
        ///     <code>
        /// public List&lt;Contractor&gt; GetContractorsAged(DateTime minDate, DateTime? maxDate)
        /// {
        ///     Fail.IfNotMidnight(minDate, "minDate must be a midnight");
        ///     Fail.IfNotMidnight(maxDate, "maxDate must be a midnight");
        /// 
        ///     // WARN: Below is sample code with no sense at all
        ///     return new List&lt;Contractor&gt;(0);
        /// }
        /// </code>
        /// </example>
        [AssertionMethod]
        public static void IfNotMidnight([CanBeNull] DateTime? value, Violation message)
        {
            if (value == null)
                return;

            DateTime dateTime = value.Value;
            Fail.IfNotEqual(dateTime.Date, dateTime, message);
        }

        /// <summary>
        /// Checks whether specified DateTime is empty - is equal to DateTime.MinValue.
        /// If it is - contract violation exception is thrown.
        /// </summary>
        /// <param name="value">DateTime to check</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static void IfEmpty(DateTime value, [NotNull] string name)
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
        public static DateTime FailIfEmpty(this DateTime value, [NotNull] string name)
        {
            Fail.IfEmpty(value, name);
            return value;
        }
    }
}