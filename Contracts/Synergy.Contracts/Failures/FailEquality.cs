using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        #region Fail.IfEqual()

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEqual<TExpected, TActual>([CanBeNull] TExpected unexpected,
            [CanBeNull] TActual actual,
            [NotNull] string message)
        {
            Fail.RequiresMessage(message);

            if (object.Equals(unexpected, actual))
                throw Fail.Because(message);
        }

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Message argument 1</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEqual<TExpected, TActual, TArgument1>([CanBeNull] TExpected unexpected,
            [CanBeNull] TActual actual,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1)
        {
            Fail.RequiresMessage(message);

            if (object.Equals(unexpected, actual))
                throw Fail.Because(message, arg1);
        }

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Message argument 1</param>
        /// <param name="arg2">Message argument 2</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEqual<TExpected, TActual, TArgument1, TArgument2>([CanBeNull] TExpected unexpected,
            [CanBeNull] TActual actual,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1,
            [CanBeNull] TArgument2 arg2)
        {
            Fail.RequiresMessage(message);

            if (object.Equals(unexpected, actual))
                throw Fail.Because(message, arg1, arg2);
        }

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Message argument 1</param>
        /// <param name="arg2">Message argument 2</param>
        /// <param name="arg3">Message argument 3</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEqual<TExpected, TActual, TArgument1, TArgument2, TArgument3>([CanBeNull] TExpected unexpected,
            [CanBeNull] TActual actual,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1,
            [CanBeNull] TArgument2 arg2,
            [CanBeNull] TArgument3 arg3)
        {
            Fail.RequiresMessage(message);

            if (object.Equals(unexpected, actual))
                throw Fail.Because(message, arg1, arg2, arg3);
        }

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEqual<TExpected, TActual>([CanBeNull] TExpected unexpected, [CanBeNull] TActual actual, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (object.Equals(unexpected, actual))
                throw Fail.Because(message, args);
        }

        #endregion

        /// <summary>
        /// Throws exception when argument value is equal to the <paramref name="unexpected"/> value.
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="argumentValue">The argument value to be checked.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [AssertionMethod]
        public static void IfArgumentEqual<TExpected, TActual>([CanBeNull] TExpected unexpected, [CanBeNull] TActual argumentValue, [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            if (object.Equals(unexpected, argumentValue))
                throw Fail.Because("Argument '{0}' is equal to {1} and it should NOT be.", argumentName, unexpected);
        }

        // TODO:mace (from:mace @ 22-10-2016): a.FailIfEqual(b)

        /// <summary>
        /// Throws exception when two values are NOT equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one MUST also be <see langword="null" />.</para>
        /// </summary>
        /// <param name="expected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotEqual<TExpected, TActual>([CanBeNull] TExpected expected, [CanBeNull] TActual actual, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (object.Equals(expected, actual) == false)
                throw Fail.Because(message, args);
        }

        // TODO:mace (from:mace @ 22-10-2016): IfArgumentNotEqual
        // TODO:mace (from:mace @ 22-10-2016): a.FailIfNotEqual(b)
    }
}