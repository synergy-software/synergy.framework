# Synergy.Contracts

## DesignByContractViolationException:
 - Message: string
 - Data: IDictionary
 - InnerException: Exception
 - TargetSite: MethodBase
 - StackTrace: string
 - HelpLink: string
 - Source: string
 - HResult: int

## Fail:
 - Because(
      message: Violation
   ) : DesignByContractViolationException [NotNull, Pure]
 - Because(
      message: string [NotNull]
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Because<T1>(
      message: string [NotNull],
      arg1: T1
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Because<T1, T2>(
      message: string [NotNull],
      arg1: T1,
      arg2: T2
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Because<T1, T2, T3>(
      message: string [NotNull],
      arg1: T1,
      arg2: T2,
      arg3: T3
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Because(
      message: string [NotNull],
      args: params Object[] [ParamArray, NotNull]
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - IfFalse(
      value: bool [AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfTrue(
      value: bool [AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - AsOrFail<T>(
      value: object [CanBeNull, NoEnumeration],
      name: string [CanBeNull, Optional]
   ) : T [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - CastOrFail<T>(
      value: object [CanBeNull, NoEnumeration],
      name: string [CanBeNull, Optional]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - IfNotCastable(
      value: object [CanBeNull, NoEnumeration],
      expectedType: Type [NotNull],
      message: Violation
   ) : Void [AssertionMethod]
 - IfNotCastable<T>(
      value: object [CanBeNull, NoEnumeration],
      message: Violation
   ) : Void [AssertionMethod]
 - IfNullOrNotCastable<T>(
      value: object [CanBeNull, NoEnumeration]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfNullOrNotCastable<T>(
      value: object [CanBeNull, NoEnumeration],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfCollectionEmpty(
      collection: IEnumerable [CanBeNull, AssertionCondition],
      collectionName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfCollectionEmpty(
      collection: IEnumerable [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - OrFailIfCollectionEmpty<T>(
      collection: T [CanBeNull, AssertionCondition],
      collectionName: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - OrFailIfCollectionEmpty<T>(
      collection: T [CanBeNull, AssertionCondition],
      message: Violation
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - IfCollectionContainsNull<T>(
      collection: IEnumerable`1 [CanBeNull, AssertionCondition],
      collectionName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfCollectionContains<T>(
      collection: IEnumerable`1 [CanBeNull, AssertionCondition],
      func: Func`2 [NotNull],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfCollectionsAreNotEquivalent<T>(
      collection1: IEnumerable`1 [CanBeNull, AssertionCondition],
      collection2: IEnumerable`1 [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfNotDate(
      date: Nullable`1 [CanBeNull],
      name: string
   ) : Void [AssertionMethod]
 - IfNotDate(
      date: Nullable`1 [CanBeNull],
      message: Violation
   ) : Void [AssertionMethod]
 - FailIfNotDate(
      date: Nullable`1 [CanBeNull],
      name: string
   ) : Nullable`1 [Extension, CanBeNull, AssertionMethod]
 - FailIfNotDate(
      date: DateTime,
      name: string
   ) : DateTime [Extension, AssertionMethod]
 - IfEmpty(
      value: DateTime,
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - FailIfEmpty(
      value: DateTime,
      name: string [NotNull]
   ) : DateTime [Extension, AssertionMethod]
 - BecauseEnumOutOfRange<T>(
      value: T
   ) : DesignByContractViolationException [NotNull, Pure]
 - IfEnumNotDefined<T>(
      value: object [NotNull]
   ) : Void
 - IfEnumNotDefined<T>(
      value: T
   ) : Void
 - FailIfEnumOutOfRange<T>(
      value: Enum [NotNull],
      name: string [NotNull]
   ) : T [Extension, NotNull]
 - CastEnumOrFail<T>(
      value: Enum [CanBeNull, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull]
 - IfEqual<TExpected, TActual>(
      unexpected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - IfEqual<TExpected, TActual>(
      unexpected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      message: Violation
   ) : Void [AssertionMethod]
 - IfArgumentEqual<TExpected, TActual>(
      unexpected: TExpected [CanBeNull],
      argumentValue: TActual [CanBeNull],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod]
 - IfNotEqual<TExpected, TActual>(
      expected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - IfNotEqual<TExpected, TActual>(
      expected: TExpected [CanBeNull],
      actual: TActual [CanBeNull],
      message: Violation
   ) : Void [AssertionMethod]
 - IfEmpty(
      value: Guid,
      message: Violation
   ) : Void [AssertionMethod]
 - IfArgumentEmpty(
      value: Guid,
      argumentName: string [NotNull]
   ) : Void [AssertionMethod]
 - FailIfNull<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - FailIfNull<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      message: Violation
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - OrFail<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - OrFail<T>(
      value: Nullable`1,
      name: string [NotNull]
   ) : T [Extension, ContractAnnotation]
 - NotNull<T>(
      value: T [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - CanBeNull<T>(
      value: T [CanBeNull, NoEnumeration]
   ) : T [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - IfArgumentNull<T>(
      argumentValue: T [CanBeNull, AssertionCondition, NoEnumeration],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfNull<T>(
      value: T [CanBeNull, AssertionCondition],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfNull<T>(
      value: T [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfNotNull<T>(
      value: T [CanBeNull, NoEnumeration],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfNotNull<T>(
      value: T [CanBeNull, NoEnumeration],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfArgumentEmpty(
      argumentValue: string [CanBeNull, AssertionCondition],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfEmpty(
      value: string [CanBeNull, AssertionCondition],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfEmpty(
      value: string [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfArgumentWhiteSpace(
      argumentValue: string [CanBeNull, AssertionCondition],
      argumentName: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - OrFailIfWhiteSpace(
      value: string [CanBeNull, AssertionCondition, NoEnumeration],
      name: string [NotNull]
   ) : string [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - IfWhitespace(
      value: string [CanBeNull, AssertionCondition],
      name: string [NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfWhitespace(
      value: string [CanBeNull, AssertionCondition],
      message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - IfTooLong(
      value: string [CanBeNull],
      maxLength: int,
      name: string [NotNull]
   ) : Void [AssertionMethod]
 - OrFailIfTooLong(
      value: string [CanBeNull],
      maxLength: int,
      name: string [NotNull]
   ) : string [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - IfTooLongOrWhitespace(
      value: string [CanBeNull],
      maxLength: int,
      name: string [NotNull]
   ) : Void [AssertionMethod]

## Violation (struct):
 - ToString() : string
 - Of(
      message: string [NotNull],
      args: params Object[] [ParamArray, NotNull]
   ) : Violation [StringFormatMethod]
 - Equals(
      obj: object
   ) : bool [SecuritySafeCritical]
 - GetHashCode() : int [SecuritySafeCritical]

