using System;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        /// <summary>
        ///     Return the exception to be thrown when enum value is not supported.
        ///     <para>REMARKS: It is usually used in switch statements (<see langword="default" />).</para>
        /// </summary>
        /// <param name="value">Value of the enum</param>
        /// <returns>Exception to throw.</returns>
        /// <example>
        ///     <code>
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
        [NotNull, Pure] [return: System.Diagnostics.CodeAnalysis.NotNull]
        public static DesignByContractViolationException BecauseEnumOutOfRange<T>(T value)
            where T : struct
        {
            // ReSharper disable once HeapView.BoxingAllocation
            return Fail.Because(Violation.WhenEnumOutOfRange<T>(null, value));
        }

        //[NotNull, Pure]
        //private static DesignByContractViolationException CreateEnumException<T>([NotNull] object value, [CanBeNull] string name = null)
        //{
        //    string enumType = typeof(T).Name;
        //    string enumValue = value.ToString();
        //    name = name ?? "enum";
        //    return new DesignByContractViolationException($"Unsupported {name} value: {enumValue} ({enumType})");
        //}

        /// <summary>
        ///     Checks whether specified value can be used as (cast to) an enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum to check</typeparam>
        /// <param name="value">Value of enum to check</param>
        public static void IfEnumNotDefined<T>([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] object value)
        {
            if (Enum.IsDefined(typeof(T), value) == false)
            {
                throw Fail.Because(Violation.WhenEnumOutOfRange<T>(null, value));
            }
        }

        /// <summary>
        /// Checks whether specified enum value is defined.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public static void IfEnumNotDefined<T>(T value) where T : struct
        {
            // ReSharper disable once HeapView.BoxingAllocation
            if (Enum.IsDefined(typeof(T), value) == false)
            {
                throw Fail.Because(Violation.WhenEnumOutOfRange<T>(null, value));
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [NotNull]
        [return: System.Diagnostics.CodeAnalysis.NotNull]
        public static T FailIfEnumOutOfRange<T>(
            this Enum value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")]
            string? name = null
#else
            string name
#endif
        )
        {
            return value.CastEnumOrFail<T>(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [NotNull]
        [return: System.Diagnostics.CodeAnalysis.NotNull]
        public static T CastEnumOrFail<T>(
            [CanBeNull] [NoEnumeration] this Enum value,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")]
            string? name = null
#else
            string name
#endif
        )
        {
            Fail.RequiresArgumentName(name);

            Type type = value.GetType();
            if (Enum.IsDefined(type, value) == false)
            {
                throw Fail.Because(Violation.WhenEnumOutOfRange<T>(name, value));
            }

            return value.CastOrFail<T>(name);
        }

        //[ExcludeFromCodeCoverage]
        //private static void RequiresEnumValue([NotNull] Enum value)
        //{
        //    if (value == null)
        //        throw new ArgumentNullException(nameof(value));
        //}
    }
}