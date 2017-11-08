using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    // TODO:mace (from:mace @ 22-10-2016): Add [AssertionCondition] below
    public static partial class Fail
    {
        private const string notCastableMessage = "Expected object of type '{0}' but was '{1}'";
        private const string notCastableMessageWithName = "Expected {0} of type '{1}' but was '{2}'";


        /// <summary>
        /// Throws exception when specified value is not castable to the specified type. It also returns the casted object or <see langword="null"/>.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and it will NOT throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="name">Name of the object to cast.</param>
        /// <returns>The casted object (or <see langword="null"/>).</returns>
        [ContractAnnotation("value: null => null; value: notnull => notnull")]
        [CanBeNull]
        [AssertionMethod]
        public static T AsOrFail<T>([CanBeNull] [NoEnumeration] this object value, [CanBeNull] string name = null)
        {
            Fail.IfNotCastable<T>(value, Fail.notCastableMessageWithName, name ?? "object", typeof(T), value);

            return (T) value;
        }

        // TODO:mace (from:mace @ 22-10-2016): public static void Fail.IfArgumentNotCastable<T>([CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue)

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type. It also returns the casted object.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="name">Name of the object to cast.</param>
        /// <returns>The casted object. This method will NEVER return <see langword="null"/>.</returns>
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [NotNull]
        [AssertionMethod]
        public static T CastOrFail<T>([CanBeNull] [NoEnumeration] this object value, [CanBeNull] string name = null)
        {
            Type castedType = typeof(T);
            Fail.IfNull(value, Fail.notCastableMessageWithName, name ?? "object", castedType, "null");

            if (castedType.IsEnum)
            {
                Fail.IfEnumNotDefined<T>(value);
                return (T) Enum.ToObject(castedType, value);
            }

            Fail.IfNotCastable<T>(value, Fail.notCastableMessageWithName, name ?? "object", castedType, value);
            return (T) value;
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and it will NOT throw the exception.</para>
        /// </summary>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotCastable([CanBeNull] [NoEnumeration] object value, [NotNull] Type expectedType, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresType(expectedType);
            Fail.RequiresMessage(message, args);

            if (value == null)
                return;

            if (expectedType.IsInstanceOfType(value) == false)
                throw Fail.Because(message, args);
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and it will NOT throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotCastable<T>([CanBeNull] [NoEnumeration] object value, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.IfNotCastable(value, typeof(T), message, args);
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNullOrNotCastable<T>([CanBeNull] [NoEnumeration] object value)
        {
            Fail.IfNull(value, Fail.notCastableMessage, typeof(T), "<null>");
            Fail.IfNotCastable<T>(value, Fail.notCastableMessage, typeof(T), value);
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [ContractAnnotation("value: null => halt")]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNullOrNotCastable<T>(
            [CanBeNull] [NoEnumeration] object value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            Fail.IfNull(value, message, args);
            Fail.IfNotCastable(value, typeof(T), message, args);
        }

        [ExcludeFromCodeCoverage]
        private static void RequiresType([NotNull] Type expectedType)
        {
            if (expectedType == null)
                throw new ArgumentNullException(nameof(expectedType));
        }
    }
}