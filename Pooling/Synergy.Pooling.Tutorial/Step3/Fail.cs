using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Synergy.Pooling.Tutorial.Step3
{
    static class Fail
    {
        private static readonly Pool<object[]> pool1 = new Pool<object[]>(() => new object[1]);

        /// <summary>
        /// Rzuca wyjątek gdy testowany parametr jest <see langword="null" />.
        /// </summary>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNull<TArgument1>(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL), CanBeNull] object value,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (value == null)
            {
                var formatted = Fail.Format(message, arg1);
                throw new InvalidOperationException(formatted);
            }
        }

        private static string Format<TArgument1>(
            [NotNull]string message,
            [CanBeNull] TArgument1 arg1)
        {
            var args = Fail.pool1.Get();

            try
            {
                args[0] = arg1;
                return String.Format(message, args);
            }
            finally
            {
                args[0] = null;
                Fail.pool1.Free(args);
            }
        }
    }
}
