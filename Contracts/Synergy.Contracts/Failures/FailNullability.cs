using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        #region variable.FailIfNull(nameof(variable))

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="name"></param>
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [NotNull]
        [AssertionMethod]
        public static T FailIfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            this T value,
            [NotNull] string name)
        {
            Fail.RequiresArgumentName(name);

            if (value == null)
                throw Fail.Because(Violation.WhenVariableIsNull(name));

            return value;
        }

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="message"></param>
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [NotNull]
        [AssertionMethod]
        public static T FailIfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            this T value,
            Violation message)
        {
            if (value == null)
                throw Fail.Because(message);

            return value;
        }

        #endregion

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="name">Name of the checked argument / parameter to check the nullability of.</param>
        /// <returns>Exactly the same value as provided to this method.</returns>
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [AssertionMethod]
        [NotNull]
        public static T OrFail<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            this T value,
            [NotNull] string name)
        {
            Fail.RequiresArgumentName(name);

            if (value == null)
                throw Fail.Because(Violation.WhenVariableIsNull(name));

            return value;
        }

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="name">Name of the checked argument / parameter to check the nullability of.</param>
        /// <returns>Exactly the same value as provided to this method.</returns>
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [AssertionMethod]
        [NotNull]
        public static T NotNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            this T value,
            [NotNull] string name)
        {
            return value.OrFail(name);
        }

        #region variable.CanBeNull()

        /// <summary>
        /// Returns EXACTLY the same object as method argument.
        /// It is useful when you have [NotNull] variable that you want to check against nullability as this method is marked with [CanBeNull].
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">Value to change its contract from [NotNull] to [CanBeNull].</param>
        /// <returns>Exactly the same value as provided to this method.</returns>
        [ContractAnnotation("value: null => null; value: notnull => notnull")]
        [AssertionMethod]
        [CanBeNull]
        public static T CanBeNull<T>([CanBeNull] [NoEnumeration] this T value)
        {
            return value;
        }

        #endregion

        #region Fail.IfArgumentNull

        /// <summary>
        /// Throws exception when specified argument value is <see langword="null" />.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against being <see langword="null" />.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        //[Obsolete("Use " + nameof(Fail) + "." + nameof(IfNull) + " instead.")]
        public static void IfArgumentNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            T argumentValue,
            [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            if (argumentValue == null)
                throw Fail.Because(Violation.WhenArgumentIsNull(argumentName));
        }

        #endregion

        #region Fail.IfNull

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="name">Name of the parameter / argument / variable to check.</param>
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            T value,
            [NotNull] string name)
        {
            Fail.RequiresArgumentName(name);

            if (value == null)
                throw Fail.Because(Violation.WhenVariableIsNull(name));
        }

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            T value,
            Violation message)
        {
            if (value == null)
                throw Fail.Because(message);
        }

        #endregion

        #region Fail.IfNotNull

        /// <summary>
        /// Throws exception when specified value is NOT <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being NOT <see langword="null" />.</param>
        /// <param name="name"></param>
        [ContractAnnotation("value: notnull => halt")]
        [AssertionMethod]
        public static void IfNotNull<T>([CanBeNull] [NoEnumeration] T value, [NotNull] string name)
        {
            Fail.RequiresArgumentName(name);

            if (value != null)
                throw Fail.Because(Violation.WhenVariableIsNotNull(name));
        }

        /// <summary>
        /// Throws exception when specified value is NOT <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being NOT <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [ContractAnnotation("value: notnull => halt")]
        [AssertionMethod]
        public static void IfNotNull<T>([CanBeNull] [NoEnumeration] T value, Violation message)
        {
            if (value != null)
                throw Fail.Because(message);
        }

        #endregion
    }
}