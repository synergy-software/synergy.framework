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

            string enumType = typeof(T).Name;
            string enumName = value.ToString();
            return new DesignByContractViolationException($"Unsupported {enumType} value: {enumName}");
        }

        //[ExcludeFromCodeCoverage]
        //private static void RequiresEnumValue([NotNull] Enum value)
        //{
        //    if (value == null)
        //        throw new ArgumentNullException(nameof(value));
        //}
    }
}