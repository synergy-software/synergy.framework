using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [NotNull]
        [AssertionMethod]
        public static T FailIfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] this T value,
            [NotNull] string message, 
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value == null)
                throw Fail.Because(message, args);

            return value;
        }

        ///// <summary>
        ///// Template for expanding <c>variable.FailIfNull(nameof(variable))</c>.
        ///// Type <c>variable.fin</c> and press TAB and let Resharper complete the template.
        ///// </summary>
        ///// <param name="value">Value to check against nullability.</param>
        //[SourceTemplate]
        //[UsedImplicitly]
        //// ReSharper disable once InconsistentNaming
        //public static void fin<T>(this T value)
        //{
        //    value.FailIfNull(nameof(value));
        //}

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
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] this T value,
            [NotNull] string name)
        {
            Fail.RequiresArgumentName(name);

            if (value == null)
                throw Fail.Because("'{0}' is null and it shouldn't be", name);

            return value;
        }

        /// <summary>
        /// Returns EXACTLY the same object as method argument.
        /// It is usefull when you have [NotNull] variable that you want to check against nullability as this method is marked with [CanBeNull].
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

        #region Fail.IfArgumentNull

        /// <summary>
        /// Throws exception when specified argument value is <see langword="null" />.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against being <see langword="null" />.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] T argumentValue,
            [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            if (argumentValue == null)
                throw Fail.Because("Argument '{0}' was null.", argumentName);
        }

        /// <summary>
        /// Template for expanding <c>Fail.IfArgumentNull(argument, nameof(argument));</c> using Resharper.
        /// Type <c>argument.fian</c> and press TAB and let Resharper complete the template.
        /// Do NOT call the method directly.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against being <see langword="null" />.</param>
        [SourceTemplate]
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        public static void fian([CanBeNull] [NoEnumeration] this object argumentValue)
        {
            Fail.IfArgumentNull(argumentValue, nameof(argumentValue));
        }

        #endregion

        #region Fail.IfNull

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value,
            [NotNull] string message)
        {
            Fail.RequiresMessage(message);

            if (value == null)
                throw Fail.Because(message);
        }

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Message argument 1</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull<T, TArgument1>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1)
        {
            Fail.RequiresMessage(message);

            if (value == null)
                throw Fail.Because(message, arg1);
        }

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Message argument 1</param>
        /// <param name="arg2">Message argument 2</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull<T, TArgument1, TArgument2>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1,
            [CanBeNull] TArgument2 arg2 )
        {
            Fail.RequiresMessage(message);

            if (value == null)
                throw Fail.Because(message, arg1, arg2);
        }

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Message argument 1</param>
        /// <param name="arg2">Message argument 2</param>
        /// <param name="arg3">Message argument 3</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull<T, TArgument1, TArgument2, TArgument3>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1,
            [CanBeNull] TArgument2 arg2,
            [CanBeNull] TArgument3 arg3)
        {
            Fail.RequiresMessage(message);

            if (value == null)
                throw Fail.Because(message, arg1, arg2, arg3);
        }

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value == null)
                throw Fail.Because(message, args);
        }

        #endregion

        /// <summary>
        /// Throws exception when specified value is NOT <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being NOT <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: notnull => halt")]
        [AssertionMethod]
        public static void IfNotNull<T>([CanBeNull] [NoEnumeration] T value, [NotNull] string message, [NotNull] params object[] args)
        {
            // TODO:mace (from:mace on:17-11-2016) This method should be splitted to 5 generic methods to prevent unnecesesary memory allocation 
            Fail.RequiresMessage(message, args);

            if (value != null)
                throw Fail.Because(message, args);
        }
    }
}