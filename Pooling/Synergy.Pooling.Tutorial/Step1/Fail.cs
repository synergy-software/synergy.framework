using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Synergy.Pooling.Tutorial.Step1
{
    static class Fail
    {
        /// <summary>
        /// Rzuca wyjątek gdy testowany parametr jest <see langword="null" />.
        /// </summary>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNull(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL), CanBeNull] object value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (value == null)
                throw new InvalidOperationException(String.Format(message, args));
        }
    }
}
