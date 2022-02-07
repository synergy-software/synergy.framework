using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    /// <summary>
    /// The exception thrown when some contract check failed.
    /// When you see it it means that someone does not meet the contract.
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
#if INTERNALS
    internal
#else
    public
#endif
        class DesignByContractViolationException : Exception
    {
        /// <summary>
        /// Constructs the exception with no message.
        /// </summary>
        public DesignByContractViolationException()
        {
        }

        /// <summary>
        /// Constructs the exception with a message.
        /// </summary>
        /// <param name="message"></param>
        public DesignByContractViolationException([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message) : base(message)
        {
        }

        /// <summary>
        /// Serialization required constructor.
        /// </summary>
        protected DesignByContractViolationException([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}