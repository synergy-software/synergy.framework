# Synergy.Contracts

## DesignByContractViolationException : Exception
 - Message: string
 - Data: IDictionary
 - InnerException: Exception
 - TargetSite: MethodBase
 - StackTrace: string
 - HelpLink: string
 - Source: string
 - HResult: int

## Fail
 - Fail.Because(
      message: Violation
   ) : DesignByContractViolationException [NotNull, Pure]
 - Fail.Because(
      message: string [NotNull]
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1>(
      message: string [NotNull],
      arg1: T1
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1, T2>(
      message: string [NotNull],
      arg1: T1,
      arg2: T2
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1, T2, T3>(
      message: string [NotNull],
      arg1: T1,
      arg2: T2,
      arg3: T3
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because(
      message: string [NotNull],
      args: params Object[] [ParamArray, NotNull]
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.IfFalse(
      value: bool [AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfTrue(
      value: bool [AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.AsOrFail<T>(
      value: object [CanBeNull, NoEnumeration],
      name: string [CanBeNull, Optional]
   ) : T [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.CastOrFail<T>(
      value: object [CanBeNull, NoEnumeration],
      name: string [CanBeNull, Optional]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.IfNotCastable(
      value: object [CanBeNull, NoEnumeration],
      expectedType: Type [NotNull],
      message: Violation
   ) : Void [AssertionMethod]
 - Fail.IfNotCastable<T>(
      value: object [CanBeNull, NoEnumeration],
      message: Violation
   ) : Void [AssertionMethod]
 - Fail.IfNullOrNotCastable<T>(
      value: object [CanBeNull, NoEnumeration]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNullOrNotCastable<T>(
      value: object [CanBeNull, NoEnumeration],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionEmpty(
      collection: IEnumerable [CanBeNull, AssertionCondition],
      collectionName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionEmpty(
      collection: IEnumerable [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfCollectionEmpty<T>(
      collection: T [CanBeNull, AssertionCondition],
      collectionName: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfCollectionEmpty<T>(
      collection: T [CanBeNull, AssertionCondition],
      message: Violation
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionContainsNull<T>(
      collection: IEnumerable`1 [CanBeNull, AssertionCondition],
      collectionName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionContains<T>(
      collection: IEnumerable`1 [CanBeNull, AssertionCondition],
      func: Func`2 [NotNull],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionsAreNotEquivalent<T>(
      collection1: IEnumerable`1 [CanBeNull, AssertionCondition],
      collection2: IEnumerable`1 [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNotDate(
      date: Nullable`1 [CanBeNull],
      name: string
   ) : Void [AssertionMethod]
 - Fail.IfNotDate(
      date: Nullable`1 [CanBeNull],
      message: Violation
   ) : Void [AssertionMethod]
 - Fail.FailIfNotDate(
      date: Nullable`1 [CanBeNull],
      name: string
   ) : Nullable`1 [Extension, CanBeNull, AssertionMethod]
 - Fail.FailIfNotDate(
      date: DateTime,
      name: string
   ) : DateTime [Extension, AssertionMethod]
 - Fail.IfEmpty(
      value: DateTime,
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - Fail.FailIfEmpty(
      value: DateTime,
      name: string [NotNull]
   ) : DateTime [Extension, AssertionMethod]
 - Fail.BecauseEnumOutOfRange<T>(
      value: T
   ) : DesignByContractViolationException [NotNull, Pure]
 - Fail.IfEnumNotDefined<T>(
      value: object [NotNull]
   ) : Void
 - Fail.IfEnumNotDefined<T>(
      value: T
   ) : Void
 - Fail.FailIfEnumOutOfRange<T>(
      value: Enum [NotNull],
      name: string [NotNull]
   ) : T [Extension, NotNull]
 - Fail.CastEnumOrFail<T>(
      value: Enum [CanBeNull, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull]
 - Fail.IfEqual<TExpected, TActual>(
      unexpected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - Fail.IfEqual<TExpected, TActual>(
      unexpected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      message: Violation
   ) : Void [AssertionMethod]
 - Fail.IfArgumentEqual<TExpected, TActual>(
      unexpected: TExpected [CanBeNull],
      argumentValue: TActual [CanBeNull],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod]
 - Fail.IfNotEqual<TExpected, TActual>(
      expected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - Fail.IfNotEqual<TExpected, TActual>(
      expected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      message: Violation
   ) : Void [AssertionMethod]
 - Fail.IfEmpty(
      value: Guid,
      message: Violation
   ) : Void [AssertionMethod]
 - Fail.IfArgumentEmpty(
      value: Guid,
      argumentName: string [NotNull]
   ) : Void [AssertionMethod]
 - Fail.FailIfNull<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.FailIfNull<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      message: Violation
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFail<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFail<T>(
      value: Nullable`1,
      name: string [NotNull]
   ) : T [Extension, ContractAnnotation]
 - Fail.NotNull<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.CanBeNull<T>(
      value: T [CanBeNull, NoEnumeration]
   ) : T [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentNull<T>(
      argumentValue: T [CanBeNull, AssertionCondition, NoEnumeration],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNull<T>(
      value: T [CanBeNull, AssertionCondition],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNull<T>(
      value: T [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNotNull<T>(
      value: T [CanBeNull, NoEnumeration],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNotNull<T>(
      value: T [CanBeNull, NoEnumeration],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentEmpty(
      argumentValue: string [CanBeNull, AssertionCondition],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfEmpty(
      value: string [CanBeNull, AssertionCondition],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfEmpty(
      value: string [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentWhiteSpace(
      argumentValue: string [CanBeNull, AssertionCondition],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfWhiteSpace(
      value: string [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : string [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.IfWhitespace(
      value: string [CanBeNull, AssertionCondition],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfWhitespace(
      value: string [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfTooLong(
      value: string [CanBeNull],
      maxLength: int,
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - Fail.OrFailIfTooLong(
      value: string [CanBeNull],
      maxLength: int,
      name: string [NotNull]
   ) : string [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.IfTooLongOrWhitespace(
      value: string [CanBeNull],
      maxLength: int,
      name: string [NotNull]
   ) : Void [AssertionMethod]

## Violation (struct)
 - Violation.Of(
      message: string [NotNull],
      args: params Object[] [ParamArray, NotNull]
   ) : Violation [StringFormatMethod]

