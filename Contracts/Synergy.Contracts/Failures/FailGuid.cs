using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        // TODO:mace (from:mace @ 22-10-2016): guid.FailIfEmpty

        /// <summary>
        /// Throws exception when checked <see cref="Guid" /> is empty ({00000000-0000-0000-0000-000000000000}).
        /// <para>REMARKS: When you create a <see cref="Guid" /> using default constructor it is empty.
        /// You can check the emptiness using this Fail.</para>
        /// </summary>
        /// <param name="value">The <see cref="Guid" /> checked for emptiness</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        public static void IfEmpty(Guid value, Violation message)
        {
            Fail.IfEqual(Guid.Empty, value, message);
        }

        /// <summary>
        /// Throws exception when checked argument is an empty <see cref="Guid" /> ({00000000-0000-0000-0000-000000000000}).<br/>
        /// <para>REMARKS: When you create a <see cref="Guid" /> using default constructor it is empty.
        /// You can check the argument emptiness using this Fail.</para>
        /// </summary>
        /// <param name="value">The <see cref="Guid" /> checked for emptiness.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        /// <example>
        /// <code>
        /// public Contractor FindContractorByGuid(Guid id)
        /// {
        ///     Fail.IfArgumentEmpty(id, nameof(id));
        /// 
        ///     ...
        /// }
        /// </code>
        /// </example>
        [AssertionMethod]
        public static void IfArgumentEmpty(Guid value, [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);
            Fail.IfEmpty(value, Violation.WhenGuidArgumentIsEmpty(argumentName));
        }
    }
}