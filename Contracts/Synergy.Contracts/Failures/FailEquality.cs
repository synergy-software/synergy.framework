using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
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
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("argumentValue")]
            string? argumentName = null
#else
            string argumentName
#endif
        )
        {
            Fail.RequiresArgumentName(argumentName);
            Fail.IfEqual(unexpected, argumentValue, Violation.WhenArgumentEqual(argumentName, unexpected));
        }
        
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
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("actual")]
            string? name = null
#else
            string name
#endif
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

        #region a.FailIfEqual(b)

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="name">Name of the checked argument / parameter to check.</param>
        [AssertionMethod]
        public static void FailIfEqual<TExpected, TActual>(
            this TActual actual,
            TExpected unexpected,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("actual")]
            string? name = null
#else
            string name
#endif
        )
        {
            Fail.RequiresArgumentName(name);
            Fail.IfEqual(unexpected, actual, Violation.WhenEqual(name, unexpected));
        }
        
        #endregion

        #region Fail.IfNotEqual()
        
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
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("actual")]
            string? name = null
#else
            string name
#endif
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
        
        #endregion
        
        #region a.FailIfNotEqual(b)

        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="name">Name of the checked argument / parameter to check.</param>
        [AssertionMethod]
        public static void FailIfNotEqual<TExpected, TActual>(
            this TActual actual,
            TExpected expected,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("actual")]
            string? name = null
#else
            string name
#endif
        )
        {
            Fail.IfNotEqual(expected, actual, name);
        }
        
        #endregion
    }
}