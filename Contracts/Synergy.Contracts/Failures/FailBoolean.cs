using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        // TODO:mace (from:mace @ 22-10-2016): variable.FailIfFalse(message)
        // TODO:mace (from:mace @ 22-10-2016): variable.FailIfTrue(message)

        #region Fail.IfFalse


        /// <summary>
        /// Throws exception when checked value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="false" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("value: false => halt")]
        public static void IfFalse(
            [AssertionCondition(AssertionConditionType.IS_TRUE)]
            bool value,
            Violation message)
        {
            if (value == false)
                throw Fail.Because(message);
        }

        #endregion

        #region Fail.IfTrue


        /// <summary>
        /// Throws exception when checked value is <see langword="true" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="true" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("value: true => halt")]
        public static void IfTrue(
            [AssertionCondition(AssertionConditionType.IS_FALSE)]
            bool value,
            Violation message)
        {
            if (value)
                throw Fail.Because(message);
        }

        #endregion
    }
}