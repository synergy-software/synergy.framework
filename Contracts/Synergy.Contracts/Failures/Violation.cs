using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    /// <summary>
    /// Holds violation message.
    /// </summary>
    public struct Violation
    {
        [NotNull] private readonly string message;
        [NotNull] private readonly object[] args;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        [StringFormatMethod("message")]
        public Violation(
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            this.message = message;
            this.args = args;
        }

        /// <summary>
        /// Returns message of the violation message.
        /// </summary>
        public override string ToString()
        {
            return String.Format(this.message, this.args);
        }

        /// <summary>
        /// Creates violation message.
        /// </summary>
        /// <param name="text">Text of the message</param>
        /// <param name="args"></param>
        /// <returns>Violation message</returns>
        [StringFormatMethod("message")]
        public static Violation Of([NotNull] string text, [NotNull] params object[] args)
        {
            return new Violation(text, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenVariableIsNull([NotNull] string name)
        {
            return Violation.Of("'{0}' is null; and it shouldn't be;", name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentName"></param>
        /// <returns></returns>
        public static Violation WhenArgumentIsNull([NotNull] string argumentName)
        {
            return Violation.Of("Argument '{0}' was null.", argumentName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenVariableIsNotNull([NotNull] string name)
        {
            return Violation.Of("'{0}' is NOT null; and it should be;", name);
        }
    }
}