using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
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
        /// <param name="args">
        ///     Arguments that will be passed to <see cref="DesignByContractViolationException" /> when the check
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
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotMidnight([CanBeNull] DateTime? value, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value == null)
                return;

            DateTime dateTime = value.Value;
            Fail.IfNotEqual(dateTime.Date, dateTime, message, args);
        }

        // TODO:mace (from:mace @ 22-10-2016): variable.FailIfNotMidnight(nameof(variable))

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
                throw Fail.Because("'{0}' is empty = {1}", name, value);
        }
    }
}