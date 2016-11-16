using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception if the specified argument is <see langword="null"/> or empty string ("").
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against <see langword="null"/> or emptiness.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            Fail.IfArgumentNull(argumentValue, argumentName);

            if (argumentValue.Length == 0)
                throw Fail.Because("Argument '{0}' was empty.", argumentName);
        }

        /// <summary>
        /// Template for expanding <c>Fail.IfArgumentEmpty(argument, nameof(argument));</c>
        /// Type <c>argument.fiae</c> and press TAB and let Resharper complete the template.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against <see langword="null"/> or emptiness.</param>
        [SourceTemplate]
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        public static void fiae([CanBeNull] this string argumentValue)
        {
            Fail.IfArgumentEmpty(argumentValue, nameof(argumentValue));
        }

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or empty string ("").
        /// </summary>
        /// <param name="value">Value to check against <see langword="null"/> or emptiness.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            Fail.IfNull(value, message, args);

            if (value.Length == 0)
                throw Fail.Because(message, args);
        }

        /// <summary>
        ///  Throws exception if the specified argument value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentWhiteSpace(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            Fail.IfArgumentNull(argumentValue, argumentName);

            if (string.IsNullOrWhiteSpace(argumentValue))
                throw Fail.Because("Argument '{0}' was empty.", argumentName);
        }

        /// <summary>
        /// Template for expanding <c>Fail.IfArgumentWhiteSpace(argument, nameof(argument));</c>
        /// Type <c>argument.fiaw</c> and press TAB and let Resharper complete the template.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check.</param>
        [SourceTemplate]
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        public static void fiaw([CanBeNull] this string argumentValue)
        {
            Fail.IfArgumentWhiteSpace(argumentValue, nameof(argumentValue));
        }

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfWhitespace(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            Fail.IfNull(value, message, args);

            if (string.IsNullOrWhiteSpace(value))
                throw Fail.Because(message, args);
        }

        /// <summary>
        /// Checks if argument name was provided.
        /// </summary>
        [ExcludeFromCodeCoverage]
        private static void RequiresArgumentName([NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
                throw new ArgumentNullException(nameof(argumentName));
        }
    }
}