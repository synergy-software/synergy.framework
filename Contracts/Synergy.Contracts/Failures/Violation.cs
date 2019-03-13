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
        /// <param name="message">Text of the message</param>
        /// <param name="args">Arguments array that contains 0 or more parameters to format a message</param>
        /// <returns>Violation message</returns>
        [StringFormatMethod("message")]
        public static Violation Of([NotNull] string message, [NotNull] params object[] args)
        {
            return new Violation(message, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenVariableIsNull([NotNull] string name) => Violation.Of("'{0}' is null; and it shouldn't be;", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentName"></param>
        /// <returns></returns>
        public static Violation WhenArgumentIsNull([NotNull] string argumentName) => Violation.Of("Argument '{0}' is null.", argumentName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenVariableIsNotNull([NotNull] string name) => Violation.Of("'{0}' is NOT null; and it should be;", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenVariableIsFalse([NotNull] string name) => Violation.Of("'{0}' is false; and it should be true;", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenVariableIsTrue([NotNull] string name) => Violation.Of("'{0}' is true; and it should be false;", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenArgumentEmpty([NotNull] string name) => Violation.Of("Argument '{0}' is empty.", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenEmpty([NotNull] string name) => Violation.Of("'{0}' is empty.", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenArgumentWhitespace([NotNull] string name) => Violation.Of("Argument '{0}' is whitespace.", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Violation WhenWhitespace([NotNull] string name) => Violation.Of("'{0}' is whitespace", name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="currentLength"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Violation WhenTooLong([NotNull] string name, int currentLength, int maxLength) =>
            Violation.Of("'{0}' is too long - {1} (max: {2})", name, currentLength, maxLength);
    }
}