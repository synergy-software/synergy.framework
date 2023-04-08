using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        // TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Add variable.OrFailIfEqual(sth)
        // TODO:mace (from:mace @ 22-10-2016): a.FailIfEqual(b)
        // TODO:mace (from:mace @ 22-10-2016): IfArgumentNotEqual
        // TODO:mace (from:mace @ 22-10-2016): a.FailIfNotEqual(b)

        #region Fail.IfEqual()

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="name">Name of the checked argument / parameter to check.</param>
        [AssertionMethod]
        public static void IfEqual<TExpected, TActual>(
            [CanBeNull] TExpected unexpected,
            [CanBeNull] TActual actual,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name
        )
        {
            Fail.RequiresArgumentName(name);
            Fail.IfEqual(unexpected, actual, Violation.WhenEqual(name, unexpected));
        }

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        public static void IfEqual<TExpected, TActual>(
            [CanBeNull] TExpected unexpected,
            [CanBeNull] TActual actual,
            Violation message
        )
        {
            if (object.Equals(unexpected, actual))
                throw Fail.Because(message);
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
        public static void IfArgumentEqual<TExpected, TActual>(
            [CanBeNull] TExpected unexpected,
            [CanBeNull] TActual argumentValue,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string argumentName
        )
        {
            Fail.RequiresArgumentName(argumentName);
            Fail.IfEqual(unexpected, argumentValue, Violation.WhenArgumentEqual(argumentName, unexpected));
        }

        /// <summary>
        /// Throws exception when two values are NOT equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one MUST also be <see langword="null" />.</para>
        /// </summary>
        /// <param name="expected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="name">Name of the checked argument / parameter to check.</param>
        [AssertionMethod]
        public static void IfNotEqual<TExpected, TActual>(
            [CanBeNull] TExpected expected,
            [CanBeNull] TActual actual,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name
        )
        {
            Fail.RequiresArgumentName(name);
            Fail.IfNotEqual(expected, actual, Violation.WhenNotEqual(name, expected, actual));
        }

        /// <summary>
        /// Throws exception when two values are NOT equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one MUST also be <see langword="null" />.</para>
        /// </summary>
        /// <param name="expected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        public static void IfNotEqual<TExpected, TActual>(
            [CanBeNull] TExpected expected,
            [CanBeNull] TActual actual,
            Violation message
        )
        {
            if (object.Equals(expected, actual) == false)
                throw Fail.Because(message);
        }
    }
}