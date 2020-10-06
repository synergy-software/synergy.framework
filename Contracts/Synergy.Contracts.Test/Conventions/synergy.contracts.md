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
   ) : DesignByContractViolationException
 - Because(
      [NotNull] message: string
   ) : DesignByContractViolationException
 - Because(
      [NotNull] message: string,
      arg1: T1
   ) : DesignByContractViolationException
 - Because(
      [NotNull] message: string,
      arg1: T1,
      arg2: T2
   ) : DesignByContractViolationException
 - Because(
      [NotNull] message: string,
      arg1: T1,
      arg2: T2,
      arg3: T3
   ) : DesignByContractViolationException
 - Because(
      [NotNull] message: string,
      [ParamArray, NotNull] args: Object[]
   ) : DesignByContractViolationException
 - IfFalse(
      [AssertionCondition] value: bool,
      message: Violation
   ) : Void
 - IfTrue(
      [AssertionCondition] value: bool,
      message: Violation
   ) : Void
 - AsOrFail(
      [CanBeNull, NoEnumeration] value: Object,
      [CanBeNull, Optional] name: string
   ) : T
 - CastOrFail(
      [CanBeNull, NoEnumeration] value: Object,
      [CanBeNull, Optional] name: string
   ) : T
 - IfNotCastable(
      [CanBeNull, NoEnumeration] value: Object,
      [NotNull] expectedType: Type,
      message: Violation
   ) : Void
 - IfNotCastable(
      [CanBeNull, NoEnumeration] value: Object,
      message: Violation
   ) : Void
 - IfNullOrNotCastable(
      [CanBeNull, NoEnumeration] value: Object
   ) : Void
 - IfNullOrNotCastable(
      [CanBeNull, NoEnumeration] value: Object,
      message: Violation
   ) : Void
 - IfCollectionEmpty(
      [CanBeNull, AssertionCondition] collection: IEnumerable,
      [NotNull] collectionName: string
   ) : Void
 - IfCollectionEmpty(
      [CanBeNull, AssertionCondition] collection: IEnumerable,
      message: Violation
   ) : Void
 - OrFailIfCollectionEmpty(
      [CanBeNull, AssertionCondition] collection: T,
      [NotNull] collectionName: string
   ) : T
 - OrFailIfCollectionEmpty(
      [CanBeNull, AssertionCondition] collection: T,
      message: Violation
   ) : T
 - IfCollectionContainsNull(
      [CanBeNull, AssertionCondition] collection: IEnumerable`1,
      [NotNull] collectionName: string
   ) : Void
 - IfCollectionContains(
      [CanBeNull, AssertionCondition] collection: IEnumerable`1,
      [NotNull] func: Func`2,
      message: Violation
   ) : Void
 - IfCollectionsAreNotEquivalent(
      [CanBeNull, AssertionCondition] collection1: IEnumerable`1,
      [CanBeNull, AssertionCondition] collection2: IEnumerable`1,
      message: Violation
   ) : Void
 - IfNotDate(
      [CanBeNull] date: Nullable`1,
      name: string
   ) : Void
 - IfNotDate(
      [CanBeNull] date: Nullable`1,
      message: Violation
   ) : Void
 - FailIfNotDate(
      [CanBeNull] date: Nullable`1,
      name: string
   ) : Nullable`1
 - FailIfNotDate(
      date: DateTime,
      name: string
   ) : DateTime
 - IfEmpty(
      value: DateTime,
      [NotNull] name: string
   ) : Void
 - FailIfEmpty(
      value: DateTime,
      [NotNull] name: string
   ) : DateTime
 - BecauseEnumOutOfRange(
      value: T
   ) : DesignByContractViolationException
 - IfEnumNotDefined(
      [NotNull] value: Object
   ) : Void
 - IfEnumNotDefined(
      value: T
   ) : Void
 - FailIfEnumOutOfRange(
      [NotNull] value: Enum,
      [NotNull] name: string
   ) : T
 - CastEnumOrFail(
      [CanBeNull, NoEnumeration] value: Enum,
      [NotNull] name: string
   ) : T
 - IfEqual(
      [CanBeNull] unexpected: TExpected,
      [CanBeNull] actual: TActual,
      [NotNull] name: string
   ) : Void
 - IfEqual(
      [CanBeNull] unexpected: TExpected,
      [CanBeNull] actual: TActual,
      message: Violation
   ) : Void
 - IfArgumentEqual(
      [CanBeNull] unexpected: TExpected,
      [CanBeNull] argumentValue: TActual,
      [NotNull] argumentName: string
   ) : Void
 - IfNotEqual(
      [CanBeNull] expected: TExpected,
      [CanBeNull] actual: TActual,
      [NotNull] name: string
   ) : Void
 - IfNotEqual(
      [CanBeNull] expected: TExpected,
      [CanBeNull] actual: TActual,
      message: Violation
   ) : Void
 - IfEmpty(
      value: Guid,
      message: Violation
   ) : Void
 - IfArgumentEmpty(
      value: Guid,
      [NotNull] argumentName: string
   ) : Void
 - FailIfNull(
      [CanBeNull, AssertionCondition, NoEnumeration] value: T,
      [NotNull] name: string
   ) : T
 - FailIfNull(
      [CanBeNull, AssertionCondition, NoEnumeration] value: T,
      message: Violation
   ) : T
 - OrFail(
      [CanBeNull, AssertionCondition, NoEnumeration] value: T,
      [NotNull] name: string
   ) : T
 - OrFail(
      value: Nullable`1,
      [NotNull] name: string
   ) : T
 - NotNull(
      [CanBeNull, AssertionCondition, NoEnumeration] value: T,
      [NotNull] name: string
   ) : T
 - CanBeNull(
      [CanBeNull, NoEnumeration] value: T
   ) : T
 - IfArgumentNull(
      [CanBeNull, AssertionCondition, NoEnumeration] argumentValue: T,
      [NotNull] argumentName: string
   ) : Void
 - IfNull(
      [CanBeNull, AssertionCondition] value: T,
      [NotNull] name: string
   ) : Void
 - IfNull(
      [CanBeNull, AssertionCondition] value: T,
      message: Violation
   ) : Void
 - IfNotNull(
      [CanBeNull, NoEnumeration] value: T,
      [NotNull] name: string
   ) : Void
 - IfNotNull(
      [CanBeNull, NoEnumeration] value: T,
      message: Violation
   ) : Void
 - IfArgumentEmpty(
      [CanBeNull, AssertionCondition] argumentValue: string,
      [NotNull] argumentName: string
   ) : Void
 - IfEmpty(
      [CanBeNull, AssertionCondition] value: string,
      [NotNull] name: string
   ) : Void
 - IfEmpty(
      [CanBeNull, AssertionCondition] value: string,
      message: Violation
   ) : Void
 - IfArgumentWhiteSpace(
      [CanBeNull, AssertionCondition] argumentValue: string,
      [NotNull] argumentName: string
   ) : Void
 - OrFailIfWhiteSpace(
      [CanBeNull, AssertionCondition, NoEnumeration] value: string,
      [NotNull] name: string
   ) : string
 - IfWhitespace(
      [CanBeNull, AssertionCondition] value: string,
      [NotNull] name: string
   ) : Void
 - IfWhitespace(
      [CanBeNull, AssertionCondition] value: string,
      message: Violation
   ) : Void
 - IfTooLong(
      [CanBeNull] value: string,
      maxLength: int,
      [NotNull] name: string
   ) : Void
 - OrFailIfTooLong(
      [CanBeNull] value: string,
      maxLength: int,
      [NotNull] name: string
   ) : string
 - IfTooLongOrWhitespace(
      [CanBeNull] value: string,
      maxLength: int,
      [NotNull] name: string
   ) : Void

## struct Violation:
 - ToString() : string
 - Of(
      [NotNull] message: string,
      [ParamArray, NotNull] args: Object[]
   ) : Violation
 - Equals(
      obj: Object
   ) : bool
 - GetHashCode() : int

