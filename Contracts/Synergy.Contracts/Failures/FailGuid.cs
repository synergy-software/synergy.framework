using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        /// <summary>
        /// Throws exception when checked <see cref="Guid" /> is empty ({00000000-0000-0000-0000-000000000000}).
        /// <para>REMARKS: When you create a <see cref="Guid" /> using default constructor it is empty.
        /// You can check the emptiness using this Fail.</para>
        /// </summary>
        /// <param name="value">The <see cref="Guid" /> checked for emptiness</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        public static void IfEmpty(
            Guid value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")] string? name = null
#else
            string name
#endif
            )
        {
            Fail.RequiresArgumentName(name);
            Fail.IfEmpty(value, Violation.WhenEmpty(name));
        }
        
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
        /// Throws exception when checked <see cref="Guid" /> is empty ({00000000-0000-0000-0000-000000000000}).
        /// <para>REMARKS: When you create a <see cref="Guid" /> using default constructor it is empty.
        /// You can check the emptiness using this Fail.</para>
        /// </summary>
        /// <param name="value">The <see cref="Guid" /> checked for emptiness</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        public static void FailIfEmpty(this Guid value, Violation message)
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
        public static void IfArgumentEmpty(
            Guid value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")]
            string? argumentName = null
#else
            string argumentName
#endif
        )
        {
            Fail.RequiresArgumentName(argumentName);
            Fail.IfEmpty(value, Violation.WhenGuidArgumentIsEmpty(argumentName));
        }
    }
}