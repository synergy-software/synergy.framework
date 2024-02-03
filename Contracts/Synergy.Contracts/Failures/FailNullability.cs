using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        // TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Use [CallerExpression] here - check https://andrewlock.net/exploring-dotnet-6-part-11-callerargumentexpression-and-throw-helpers/
        
        #region variable.FailIfNull(nameof(variable))

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        [NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull] 
        [AssertionMethod]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        public static T FailIfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            this T value,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.RequiresArgumentName(name);
            return value.FailIfNull(Violation.WhenVariableIsNull(name));
        }

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
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
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <returns>Exactly the same value as provided to this method.</returns>
        [NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        public static T OrFail<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            this T value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")] string? name = null
#else
            string name
#endif
            )
        {
            Fail.RequiresArgumentName(name);

            if (value == null)
                throw Fail.Because(Violation.WhenVariableIsNull(name));

            return value;
        }

        /// <summary>
        /// /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <returns>Value (not null) of the passed argument / parameter</returns>
        /// <exception cref="DesignByContractViolationException"></exception>
        [ContractAnnotation("value: null => halt")]
        public static T OrFail<T>(this T? value, [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name) where T : struct
        {
            Fail.RequiresArgumentName(name);
            
            if (value == null)
                throw Fail.Because(Violation.WhenVariableIsNull(name));

            return value.Value;
        }
        
        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="name">Name of the checked argument / parameter.</param>
        /// <returns>Exactly the same value as provided to this method.</returns>
        [NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        public static T NotNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            this T value,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name
            )
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
        [CanBeNull]
        [AssertionMethod]
        [ContractAnnotation("value: null => null; value: notnull => notnull")]
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
        [AssertionMethod]
        [ContractAnnotation("argumentValue: null => halt")]
        //[Obsolete("Use " + nameof(Fail) + "." + nameof(IfNull) + " instead.")]
        public static void IfArgumentNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration]
            T argumentValue,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string argumentName)
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
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            T value,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.RequiresArgumentName(name);
            Fail.IfNull(value, Violation.WhenVariableIsNull(name));
        }

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static void IfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value,
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
        /// <param name="name">Name of the checked argument / parameter.</param>
        [AssertionMethod]
        [ContractAnnotation("value: notnull => halt")]
        public static void IfNotNull<T>([CanBeNull] [NoEnumeration] T value, [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string name)
        {
            Fail.RequiresArgumentName(name);
            Fail.IfNotNull(value, Violation.WhenVariableIsNotNull(name));
        }

        /// <summary>
        /// Throws exception when specified value is NOT <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being NOT <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("value: notnull => halt")]
        public static void IfNotNull<T>([CanBeNull] [NoEnumeration] T value, Violation message)
        {
            if (value != null)
                throw Fail.Because(message);
        }

        #endregion
    }
}