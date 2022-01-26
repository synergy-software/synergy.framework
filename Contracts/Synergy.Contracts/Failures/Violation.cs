using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    /// <summary>
    /// Holds violation message.
    /// </summary>
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public readonly struct Violation
    {
        [JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull]
        private readonly string message;

        [JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull]
        private readonly object[] args;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        [StringFormatMethod("message")]
        public Violation(
            [JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message,
            [JetBrains.Annotations.NotNull]  [System.Diagnostics.CodeAnalysis.NotNull] params object[] args)
        {
            this.message = message;
            this.args = args;
        }

        /// <summary>
        /// Returns message of the violation message.
        /// </summary>
        [JetBrains.Annotations.NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull]
        public override string ToString()
        {
            if (this.args.Length == 0)
                return this.message;
            
            return string.Format(this.message, this.args);
        }

        /// <summary>
        /// Creates violation message.
        /// </summary>
        /// <param name="message">Text of the message</param>
        /// <param name="args">Arguments array that contains 0 or more parameters to format a message</param>
        /// <returns>Violation message</returns>
        [StringFormatMethod("message")]
        public static Violation Of([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string message, [JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] params object[] args)
        {
            return new Violation(message, args);
        }

        /// <summary>
        /// "'{0}' is null; and it shouldn't be;"
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenVariableIsNull([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("'{0}' is null; and it shouldn't be;", name);

        /// <summary>
        /// "Argument '{0}' is null."
        /// </summary>
        /// <param name="argumentName"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenArgumentIsNull([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string argumentName) => Violation.Of("Argument '{0}' is null.", argumentName);

        /// <summary>
        /// "'{0}' is NOT null; and it should be;"
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenVariableIsNotNull([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("'{0}' is NOT null; and it should be;", name);

        /// <summary>
        /// "'{0}' is false; and it should be true;"
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenVariableIsFalse([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("'{0}' is false; and it should be true;", name);

        /// <summary>
        /// "'{0}' is true; and it should be false;"
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenVariableIsTrue([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("'{0}' is true; and it should be false;", name);

        /// <summary>
        /// "Argument '{0}' is empty."
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenArgumentEmpty([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("Argument '{0}' is empty.", name);

        /// <summary>
        /// "'{0}' is empty."
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenEmpty([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("'{0}' is empty.", name);

        /// <summary>
        /// "Argument '{0}' is whitespace."
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenArgumentWhitespace([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("Argument '{0}' is whitespace.", name);

        /// <summary>
        /// "'{0}' is whitespace"
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenWhitespace([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("'{0}' is whitespace", name);

        /// <summary>
        /// "'{0}' is too long - {1} (max: {2})"
        /// </summary>
        /// <param name="name"></param>
        /// <param name="currentLength"></param>
        /// <param name="maxLength"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenTooLong([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name, int currentLength, int maxLength) =>
            Violation.Of("'{0}' is too long - {1} (max: {2})", name, currentLength, maxLength);

        /// <summary>
        /// '{0}' is equal to {1} and it should NOT be.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="unexpected"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenEqual<T>([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name, [CanBeNull] T unexpected) =>
            Violation.Of("'{0}' is equal to {1} and it should NOT be.", name, Violation.FormatValue(unexpected));

        /// <summary>
        /// "Argument '{0}' is equal to {1} and it should NOT be."
        /// </summary>
        /// <param name="name"></param>
        /// <param name="unexpected"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenArgumentEqual<T>([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name, [CanBeNull] T unexpected) =>
            Violation.Of("Argument '{0}' is equal to {1} and it should NOT be.", name, Violation.FormatValue(unexpected));

        /// <summary>
        /// '{0}' is NOT equal to {1} and it should be.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenNotEqual<TExpected, TActual>([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name, [CanBeNull] TExpected expected, [CanBeNull] TActual actual) =>
            Violation.Of("'{0}' ({2}) is NOT equal to {1} and it should be.", name, Violation.FormatValue(expected), Violation.FormatValue(actual));

        /// <summary>
        /// "Argument '{0}' is an empty Guid."
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenGuidArgumentIsEmpty([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) => Violation.Of("Argument '{0}' is an empty Guid.", name);

        /// <summary>
        /// "'{0}' is not date = {1}"
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenDateTimeIsNotDate(string name, DateTime value) =>
            Violation.Of("'{0}' is not date = {1}", name, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// "'{0}' is empty = {1}"
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenDateTimeIsEmpty(string name, DateTime value) =>
            Violation.Of("'{0}' is empty = {1}", name, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenEnumOutOfRange<T>([CanBeNull] string name, [JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] object value)
        {
            string enumType = typeof(T).Name;
            string enumValue = value.ToString();
            name = name ?? "enum";

            return Violation.Of($"Unsupported {name} value: {enumValue} ({enumType})");
        }

        /// <summary>
        /// "Collection '{0}' should not be null but it is."
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenCollectionIsNull([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string collectionName) =>
            Violation.Of("Collection '{0}' should not be null but it is.", collectionName);

        /// <summary>
        /// "Collection '{0}' should not be empty but it is."
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenCollectionIsEmpty([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string collectionName) =>
            Violation.Of("Collection '{0}' should not be empty but it is.", collectionName);

        /// <summary>
        /// "Collection '{0}' contains null"
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns>Violation message</returns>
        internal static Violation WhenCollectionContainsNull([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string collectionName) => 
            Violation.Of("Collection '{0}' contains null", collectionName);

        /// <summary>
        /// "Expected {0} of type '{1}' but was '{2}'"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static Violation WhenCannotCast<T>([JetBrains.Annotations.NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name, [CanBeNull] object value) =>
            Violation.Of("Expected {0} of type '{1}' but was '{2}'", name, typeof(T), FormatValue(value));

        [JetBrains.Annotations.NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull]
        private static string FormatValue<T>([CanBeNull] T value)
        {
            if (value == null)
                return "null";

            return value.ToString();
        }
    }
}