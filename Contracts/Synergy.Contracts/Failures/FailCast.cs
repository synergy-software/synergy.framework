using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    // TODO:mace (from:mace @ 22-10-2016): Add [AssertionCondition] below
    public static partial class Fail
    {
        // TODO:mace (from:mace @ 22-10-2016): public static void Fail.IfArgumentNotCastable<T>([CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue)

        /// <summary>
        /// Throws exception when specified value is not cast to the specified type. It also returns the cast object or <see langword="null"/>.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and it will NOT throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="value">Value to check if it can be cast to specified type.</param>
        /// <param name="name">Name of the object to cast.</param>
        /// <returns>The cast object (or <see langword="null"/>).</returns>
        [CanBeNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => null; value: notnull => notnull")]
        public static T AsOrFail<T>([CanBeNull] [NoEnumeration] this object value, [CanBeNull] string name = null)
        {
            Fail.IfNotCastable<T>(value, Violation.WhenCannotCast<T>(name ?? "object", value));

            return (T) value;
        }

        /// <summary>
        /// Throws exception when specified value is not cast to the specified type. It also returns the cast object.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="value">Value to check if it can be cast to specified type.</param>
        /// <param name="name">Name of the object to cast.</param>
        /// <returns>The cast object. This method will NEVER return <see langword="null"/>.</returns>
        [NotNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        public static T CastOrFail<T>([CanBeNull] [NoEnumeration] this object value, [CanBeNull] string name = null)
        {
            Type castType = typeof(T);
            Fail.IfNull(value, Violation.WhenCannotCast<T>(name ?? "object", value));

            if (castType.IsEnum)
            {
                Fail.IfEnumNotDefined<T>(value);
                return (T) Enum.ToObject(castType, value);
            }

            Fail.IfNotCastable<T>(value, Violation.WhenCannotCast<T>(name ?? "object", value));
            return (T) value;
        }

        /// <summary>
        /// Throws exception when specified value is not cast to the specified type.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and it will NOT throw the exception.</para>
        /// </summary>
        /// <param name="value">Value to check if it can be cast to specified type.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        public static void IfNotCastable([CanBeNull] [NoEnumeration] object value, [NotNull] Type expectedType, Violation message)
        {
            Fail.RequiresType(expectedType);

            if (value == null)
                return;

            if (expectedType.IsInstanceOfType(value) == false)
                throw Fail.Because(message);
        }

        /// <summary>
        /// Throws exception when specified value is not cast to the specified type.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and it will NOT throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be cast to specified type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        public static void IfNotCastable<T>([CanBeNull] [NoEnumeration] object value, Violation message)
        {
            Fail.IfNotCastable(value, typeof(T), message);
        }

        /// <summary>
        /// Throws exception when specified value is not cast to the specified type.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be cast to specified type.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfNullOrNotCastable<T>([CanBeNull] [NoEnumeration] object value)
        {
            Fail.IfNull(value, Violation.WhenCannotCast<T>("object", value));
            Fail.IfNotCastable<T>(value, Violation.WhenCannotCast<T>("object", value));
        }

        /// <summary>
        /// Throws exception when specified value is not cast to the specified type.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be cast to specified type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfNullOrNotCastable<T>(
            [CanBeNull] [NoEnumeration] object value,
            Violation message)
        {
            Fail.IfNull(value, message);
            Fail.IfNotCastable(value, typeof(T), message);
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void RequiresType([NotNull] Type expectedType)
        {
            if (expectedType == null)
                throw new ArgumentNullException(nameof(expectedType));
        }
    }
}