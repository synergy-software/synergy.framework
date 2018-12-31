using JetBrains.Annotations;

namespace Synergy.Contracts
{
    /// <summary>
    /// Holds violation message.
    /// </summary>
    public struct Violation
    {
        [NotNull] 
        private readonly string text;

        /// <summary>
        /// Creates violation message.
        /// </summary>
        /// <param name="text"></param>
        private Violation([NotNull] string text)
        {
            this.text = text;
        }

        /// <summary>
        /// Creates violation message.
        /// </summary>
        /// <param name="text">Text of the message</param>
        /// <returns>Violation message</returns>
        public static Violation Message([NotNull] string text)
        {
            return new Violation(text);
        }

        /// <summary>
        /// Returns text of the violation message.
        /// </summary>
        public override string ToString()
        {
            return this.text;
        }
    }
}