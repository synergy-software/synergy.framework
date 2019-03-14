using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception when checked <see cref="Guid" /> is empty ({00000000-0000-0000-0000-000000000000}).
        /// <para>REMARKS: When you create a <see cref="Guid" /> using default constructor it is empty. You can check the emptiness using this Fail.</para>
        /// </summary>
        /// <param name="value">The <see cref="Guid" /> checked for emptiness</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEmpty(Guid value, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);
            Fail.IfEqual(Guid.Empty, value, Violation.Of(message, args));
        }

        /// <summary>
        /// Throws exception when checked argument is an empty <see cref="Guid" /> ({00000000-0000-0000-0000-000000000000}).<br/>
        /// <para>REMARKS: When you create a <see cref="Guid" /> using default constructor it is empty. You can check the argument emptiness using this Fail.</para>
        /// </summary>
        /// <param name="value">The <see cref="Guid" /> checked for emptiness.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        /// <example>
        /// <code>
        /// public Contractor FindContractorByGuid(Guid id)
        /// {
        ///     Fail.IfArgumentEmpty(id, nameof(id));
        /// 
        ///     // WARN: Below is sample code with no sense at all
        ///     return new Contractor();
        /// }
        /// </code>
        /// </example>
        [AssertionMethod]
        public static void IfArgumentEmpty(Guid value, [NotNull] [InvokerParameterName] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);
            Fail.IfEqual(Guid.Empty, value, Violation.Of("Argument '{0}' is an empty Guid.", argumentName));
        }

        // TODO:mace (from:mace @ 22-10-2016): guid.FailIfEmpty
    }
}