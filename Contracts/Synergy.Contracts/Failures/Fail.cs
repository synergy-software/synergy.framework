using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    public static partial class Fail
    {
        /// <summary>
        ///     Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <returns>The exception to throw when contract is violated.</returns>
        [NotNull]
        [Pure]
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
        [StringFormatMethod("message")]
        [NotNull]
        [Pure]
        public static DesignByContractViolationException Because([NotNull] string message)
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
        [StringFormatMethod("message")]
        [NotNull]
        [Pure]
        public static DesignByContractViolationException Because<T1>([NotNull] string message, T1 arg1)
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
        [StringFormatMethod("message")]
        [NotNull]
        [Pure]
        public static DesignByContractViolationException Because<T1, T2>([NotNull] string message, T1 arg1, T2 arg2)
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
        [StringFormatMethod("message")]
        [NotNull]
        [Pure]
        public static DesignByContractViolationException Because<T1, T2, T3>([NotNull] string message, T1 arg1, T2 arg2, T3 arg3)
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
        [StringFormatMethod("message")]
        [NotNull]
        [Pure]
        public static DesignByContractViolationException Because([NotNull] string message, [NotNull] params object[] args)
        {
            return Fail.Because(message.Formatted(args));
        }

        [ExcludeFromCodeCoverage]
        private static void RequiresMessage([NotNull] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));
        }

        [ExcludeFromCodeCoverage]
        private static void RequiresMessage([NotNull] string message, [NotNull] object[] args)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            if (args == null)
                throw new ArgumentNullException(nameof(args));
        }
    }
}