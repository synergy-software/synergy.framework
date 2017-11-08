using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Return the exception to be thrown when enum value is not supported.
        /// <para>REMARKS: It is usually used in switch statements (<see langword="default"/>).</para>
        /// </summary>
        /// <param name="value">Value of the enum</param>
        /// <returns>Exception to throw.</returns>
        /// <example>
        /// <code>
        ///  [CanBeNull, Pure]
        ///  public string GetName()
        ///  {
        ///      switch (this.Type)
        ///      {
        ///          case ContractorType.Company:
        ///              return this.CompanyName;
        ///          case ContractorType.Person:
        ///              return this.FirstName + " " + this.LastName;
        ///          default:
        ///              throw Fail.BecauseEnumOutOfRange(this.Type);
        ///      }
        ///  }
        /// </code>
        /// </example>
        [NotNull, Pure]
        public static DesignByContractViolationException BecauseEnumOutOfRange<T>(T value) where T:struct
        {
            //Fail.RequiresEnumValue(value);

            return Fail.CreateEnumException<T>(value);
        }

        [NotNull, Pure]
        private static DesignByContractViolationException CreateEnumException<T>([NotNull] object value)
        {
            string enumType = typeof(T).Name;
            string enumName = value.ToString();
            return new DesignByContractViolationException($"Unsupported {enumType} value: {enumName}");
        }

        /// <summary>
        /// Checks whether specified value can be used as (casted to) an enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum to check</typeparam>
        /// <param name="value">Value of enum to chceck</param>
        public static void IfEnumNotDefined<T>([NotNull] object value)
        {
            if (Enum.IsDefined(typeof(T), value) == false)
            {
                throw CreateEnumException<T>(value);
            }
        }

        //[ExcludeFromCodeCoverage]
        //private static void RequiresEnumValue([NotNull] Enum value)
        //{
        //    if (value == null)
        //        throw new ArgumentNullException(nameof(value));
        //}
    }
}