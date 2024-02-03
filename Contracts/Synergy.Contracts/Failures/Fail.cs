using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Synergy.Extensions;

namespace Synergy.Contracts
{
    /// <summary>
    ///     <para>
    ///         Static class for one-line contract checks. When contract is failed it throws
    ///         <see cref="DesignByContractViolationException" />.
    ///         Use it widely on every single method.
    ///     </para>
    ///     <para>
    ///         - consider each method separately - what contract this method has |
    ///         - check method arguments for contract violation in the beginning of your method |
    ///         - keep an empty line after the arguments contract checks |
    ///         - keep the contract checks one line long - do not disturb your code coverage withe the contract checks |
    ///         - do not unit test the contracts |
    ///         - use it with [NotNull] and [CanBeNull] attributes |
    ///     </para>
    ///     <para>
    ///         <strong>Design By Contract Programming is a state of mind not code.</strong>
    ///         If all is done properly the <see cref="DesignByContractViolationException" /> will never be seen on a
    ///         production environment.
    ///         The contract checks help developers clarify what is expected. During the development phase when we integrate
    ///         components
    ///         (simply: when we call method of another class) we may violate the contract and receive the exception, but this
    ///         is what it is for.
    ///         If you encounter such case, you simply need to conform the contract.
    ///     </para>
    /// </summary>
    [DebuggerStepThrough]
#if INTERNALS
    internal
#else
    public
#endif
        static partial class Fail
    {
        /// <summary>
        ///     Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <returns>The exception to throw when contract is violated.</returns>
        [NotNull, Pure] [return: System.Diagnostics.CodeAnalysis.NotNull]
        public static DesignByContractViolationException Because(Violation message)
        {
            return new DesignByContractViolationException(message.ToString());
        }

        /// <summary>
        ///     Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <returns>The exception to throw when contract is violated.</returns>
        /// <example>
        ///     <code>
        /// public void SetPersonName([NotNull] string firstName, [NotNull] string lastName)
        /// {
        ///     throw Fail.Because("Not implemented yet");
        /// }
        /// </code>
        /// </example>
        [NotNull, Pure] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [StringFormatMethod("message")]
        public static DesignByContractViolationException Because([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message)
        {
            Fail.RequiresMessage(message);

            return new DesignByContractViolationException(message);
        }

        /// <summary>
        ///     Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <returns>The exception to throw when contract is violated.</returns>
        /// <example>
        ///     <code>
        /// public void SetPersonName([NotNull] string firstName, [NotNull] string lastName)
        /// {
        ///     throw Fail.Because("Not implemented yet");
        /// }
        /// </code>
        /// </example>
        [NotNull, Pure] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [StringFormatMethod("message")]
        public static DesignByContractViolationException Because<T1>([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message, T1 arg1)
        {
            return Fail.Because(message.Formatted(arg1));
        }

        /// <summary>
        ///     Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <returns>The exception to throw when contract is violated.</returns>
        /// <example>
        ///     <code>
        /// public void SetPersonName([NotNull] string firstName, [NotNull] string lastName)
        /// {
        ///     throw Fail.Because("Not implemented yet");
        /// }
        /// </code>
        /// </example>
        [NotNull, Pure] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [StringFormatMethod("message")]
        public static DesignByContractViolationException Because<T1, T2>([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message, T1 arg1, T2 arg2)
        {
            return Fail.Because(message.Formatted(arg1, arg2));
        }

        /// <summary>
        ///     Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <returns>The exception to throw when contract is violated.</returns>
        /// <example>
        ///     <code>
        /// public void SetPersonName([NotNull] string firstName, [NotNull] string lastName)
        /// {
        ///     throw Fail.Because("Not implemented yet");
        /// }
        /// </code>
        /// </example>
        [NotNull, Pure] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [StringFormatMethod("message")]
        public static DesignByContractViolationException Because<T1, T2, T3>([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message, T1 arg1, T2 arg2, T3 arg3)
        {
            return Fail.Because(message.Formatted(arg1, arg2, arg3));
        }

        /// <summary>
        ///     Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <param name="message">Message that will be passed to the <see cref="DesignByContractViolationException" />.</param>
        /// <param name="args">Arguments that will be passed to the <see cref="DesignByContractViolationException" />.</param>
        /// <returns>The exception to throw when contract is violated.</returns>
        /// <example>
        ///     <code>
        /// public void SetPersonName([NotNull] string firstName, [NotNull] string lastName)
        /// {
        ///     throw Fail.Because("Not implemented yet");
        /// }
        /// </code>
        /// </example>
        [NotNull, Pure] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [StringFormatMethod("message")]
        public static DesignByContractViolationException Because(
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] params object[] args
        )
        {
            return Fail.Because(message.Formatted(args));
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void RequiresMessage([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));
        }

        /// <summary>
        /// Checks if argument name was provided.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void RequiresArgumentName(string? argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
                throw new ArgumentNullException(nameof(argumentName));
        }
    }
}