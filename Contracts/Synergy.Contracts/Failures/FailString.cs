using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        #region Fail.IfArgumentEmpty

        /// <summary>
        /// Throws exception if the specified argument is <see langword="null"/> or empty string ("").
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against <see langword="null"/> or emptiness.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [AssertionMethod]
        [ContractAnnotation("argumentValue: null => halt")]
        public static void IfArgumentEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);
            Fail.IfArgumentNull(argumentValue, argumentName);

            if (argumentValue.Length == 0)
                throw Fail.Because(Violation.WhenArgumentEmpty(argumentName));
        }

        #endregion

        #region Fail.IfEmpty

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or empty string ("").
        /// </summary>
        /// <param name="value">Value to check against <see langword="null"/> or emptiness.</param>
        /// <param name="name">Name of the checked argument / parameter to check.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.RequiresArgumentName(name);
            Fail.IfEmpty(value, Violation.WhenEmpty(name));
        }

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or empty string ("").
        /// </summary>
        /// <param name="value">Value to check against <see langword="null"/> or emptiness.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            Violation message)
        {
            Fail.IfNull(value, message);

            if (value.Length == 0)
                throw Fail.Because(message);
        }

        #endregion

        /// <summary>
        ///  Throws exception if the specified argument value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [AssertionMethod]
        [ContractAnnotation("argumentValue: null => halt")]
        public static void IfArgumentWhiteSpace(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);
            Fail.IfArgumentNull(argumentValue, argumentName);

            if (string.IsNullOrWhiteSpace(argumentValue))
                throw Fail.Because(Violation.WhenArgumentWhitespace(argumentName));
        }

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="value">Value to check against nullability and white space.</param>
        /// <param name="name">Name of the checked argument / parameter to check.</param>
        /// <returns>Exactly the same value as provided to this method.</returns>
        [NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        public static string OrFailIfWhiteSpace(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] this string? value,
#if NET6_0
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull][System.Runtime.CompilerServices.CallerArgumentExpression("value")] string name = ""
#else
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name
#endif
            )
        {
            Fail.RequiresArgumentName(name);

            Fail.IfWhitespace(value, name);

            return value;
        }

        #region Fail.IfWhitespace

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfWhitespace(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.RequiresArgumentName(name);
            Fail.IfWhitespace(value, Violation.WhenWhitespace(name));
        }

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfWhitespace(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            Violation message)
        {
            Fail.IfNull(value, message);

            if (string.IsNullOrWhiteSpace(value))
                throw Fail.Because(message);
        }

        #endregion

        /// <summary>
        /// Throws exception if the specified string length exceeds provided maximum.
        /// It doesn't check the null-ness of the value (null does NOT exceed any length).
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="maxLength">Maximum length of the string.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static void IfTooLong(
            [CanBeNull] string value,
            int maxLength,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.RequiresArgumentName(name);

            if (value == null)
                return;

            int currentLength = value.Length;
            if (currentLength > maxLength)
                throw Fail.Because(Violation.WhenTooLong(name, currentLength, maxLength));
        }

        /// <summary>
        /// Throws exception if the specified string length exceeds provided maximum.
        /// It doesn't check the null-ness of the value (null does NOT exceed any length).
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="maxLength">Maximum length of the string.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [CanBeNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => null")]
        public static string OrFailIfTooLong(
            [CanBeNull] this string value,
            int maxLength,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.IfTooLong(value, maxLength, name);
            return value;
        }

        /// <summary>
        /// Throws exception if the specified string length exceeds provided maximum or it is a whitespace.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="maxLength">Maximum length of the string.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static void IfTooLongOrWhitespace(
            [CanBeNull] string value,
            int maxLength,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.IfArgumentWhiteSpace(value, name);
            Fail.IfTooLong(value, maxLength, name);
        }
    }
}