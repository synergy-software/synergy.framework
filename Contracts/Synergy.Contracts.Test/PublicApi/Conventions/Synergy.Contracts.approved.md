# Synergy.Contracts

## DesignByContractViolationException : Exception
 - TargetSite: MethodBase { get; }
 - StackTrace: string { get; }
 - Message: string? [Nullable] { get; }
 - Data: IDictionary? [Nullable] { get; }
 - InnerException: Exception { get; }
 - HelpLink: string { get; set; }
 - Source: string { get; set; }
 - HResult: int { get; set; }

## Fail
 - Fail.IfTooLong(
     value: string [CanBeNull],
     maxLength: int,
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod]
 - Fail.OrFailIfTooLong(
     value: string [CanBeNull],
     maxLength: int,
     name: string [NotNull, NotNull]
   ) : string [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.IfTooLongOrWhitespace(
     value: string [CanBeNull],
     maxLength: int,
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod]
 - Fail.Because(
     message: Violation
   ) : DesignByContractViolationException [NotNull, Pure]
 - Fail.Because(
     message: string [NotNull, NotNull]
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1>(
     message: string [NotNull, NotNull],
     arg1: T1
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1, T2>(
     message: string [NotNull, NotNull],
     arg1: T1,
     arg2: T2
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1, T2, T3>(
     message: string [NotNull, NotNull],
     arg1: T1,
     arg2: T2,
     arg3: T3
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because(
     message: string [NotNull, NotNull],
     args: params Object[] [ParamArray, NotNull, NotNull]
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
     expectedType: Type [NotNull, NotNull],
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
     collectionName: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionEmpty(
     collection: IEnumerable [CanBeNull, AssertionCondition],
     message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfCollectionEmpty<T>(
     collection: T [CanBeNull, AssertionCondition],
     collectionName: string [NotNull, NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfCollectionEmpty<T>(
     collection: T [CanBeNull, AssertionCondition],
     message: Violation
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionContainsNull<T>(
     collection: IEnumerable`1 [CanBeNull, AssertionCondition],
     collectionName: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionContains<T>(
     collection: IEnumerable`1 [CanBeNull, AssertionCondition],
     func: Func`2 [NotNull, NotNull],
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
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod]
 - Fail.FailIfEmpty(
     value: DateTime,
     name: string [NotNull, NotNull]
   ) : DateTime [Extension, AssertionMethod]
 - Fail.BecauseEnumOutOfRange<T>(
     value: T
   ) : DesignByContractViolationException [NotNull, Pure]
 - Fail.IfEnumNotDefined<T>(
     value: object [NotNull, NotNull]
   ) : Void
 - Fail.IfEnumNotDefined<T>(
     value: T
   ) : Void
 - Fail.FailIfEnumOutOfRange<T>(
     value: Enum [NotNull, NotNull],
     name: string [NotNull, NotNull]
   ) : T [Extension, NotNull]
 - Fail.CastEnumOrFail<T>(
     value: Enum [CanBeNull, NoEnumeration],
     name: string [NotNull, NotNull]
   ) : T [Extension, NotNull]
 - Fail.IfEqual<TExpected, TActual>(
     unexpected: TExpected [CanBeNull],
     actual: TActual [CanBeNull],
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod]
 - Fail.IfEqual<TExpected, TActual>(
     unexpected: TExpected [CanBeNull],
     actual: TActual [CanBeNull],
     message: Violation
   ) : Void [AssertionMethod]
 - Fail.IfArgumentEqual<TExpected, TActual>(
     unexpected: TExpected [CanBeNull],
     argumentValue: TActual [CanBeNull],
     argumentName: string [NotNull, NotNull]
   ) : Void [AssertionMethod]
 - Fail.IfNotEqual<TExpected, TActual>(
     expected: TExpected [CanBeNull],
     actual: TActual [CanBeNull],
     name: string [NotNull, NotNull]
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
     name: string [NotNull, NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.FailIfNull<T>(
     value: T [CanBeNull, AssertionCondition, NoEnumeration],
     message: Violation
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFail<T>(
     value: T [CanBeNull, AssertionCondition, NoEnumeration],
     name: string [NotNull, NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFail<T>(
     value: Nullable`1,
     name: string [NotNull, NotNull]
   ) : T [Extension, ContractAnnotation]
 - Fail.NotNull<T>(
     value: T [CanBeNull, AssertionCondition, NoEnumeration],
     name: string [NotNull, NotNull]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.CanBeNull<T>(
     value: T [CanBeNull, NoEnumeration]
   ) : T [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentNull<T>(
     argumentValue: T [CanBeNull, AssertionCondition, NoEnumeration],
     argumentName: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNull<T>(
     value: T [CanBeNull, AssertionCondition],
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNull<T>(
     value: T [CanBeNull, AssertionCondition],
     message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNotNull<T>(
     value: T [CanBeNull, NoEnumeration],
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfNotNull<T>(
     value: T [CanBeNull, NoEnumeration],
     message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentEmpty(
     argumentValue: string [CanBeNull, AssertionCondition],
     argumentName: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfEmpty(
     value: string [CanBeNull, AssertionCondition],
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfEmpty(
     value: string [CanBeNull, AssertionCondition],
     message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentWhiteSpace(
     argumentValue: string [CanBeNull, AssertionCondition],
     argumentName: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfWhiteSpace(
     value: string [CanBeNull, AssertionCondition, NoEnumeration],
     name: string [NotNull, NotNull]
   ) : string [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.IfWhitespace(
     value: string [CanBeNull, AssertionCondition],
     name: string [NotNull, NotNull]
   ) : Void [AssertionMethod, ContractAnnotation]
 - Fail.IfWhitespace(
     value: string [CanBeNull, AssertionCondition],
     message: Violation
   ) : Void [AssertionMethod, ContractAnnotation]

## Violation (struct)
 - Violation.Of(
     message: string [NotNull, NotNull],
     args: params Object[] [ParamArray, NotNull, NotNull]
   ) : Violation [StringFormatMethod]

## Requirements.Business
 - Business.Rule(
     description: string
   ) : Principle [MustUseReturnValue]
 - Business.When(
     preCondition: bool
   ) : Precondition [MustUseReturnValue]
 - Business.Requires(
     condition: bool
   ) : Requirement [MustUseReturnValue]

## Requirements.BusinessRuleViolationException : Exception
 - Requirement: Requirement { get; }
 - TargetSite: MethodBase { get; }
 - StackTrace: string { get; }
 - Message: string? [Nullable] { get; }
 - Data: IDictionary? [Nullable] { get; }
 - InnerException: Exception { get; }
 - HelpLink: string { get; set; }
 - Source: string { get; set; }
 - HResult: int { get; set; }

## Requirements.Business+Precondition (struct)
 - Met: bool { get; }
 - Comment: string [CanBeNull] { get; }
 - Item: Precondition { get; }
 - And(
     preCondition: bool
   ) : Precondition [MustUseReturnValue]
 - Requires(
     condition: bool
   ) : Requirement [MustUseReturnValue]
 - Requires(
     condition: Func`1
   ) : Requirement [MustUseReturnValue]

## Requirements.Business+IPrecondition
 - Met: bool { get; }

## Requirements.Business+Requirement (struct)
 - Comment: string [CanBeNull] { get; }
 - Met: bool { get; }
 - Item: Requirement { get; }
 - Throws(
     message: string
   ) : Void
 - Throws(
     exception: Exception
   ) : Void

## Requirements.Business+Principle (struct)
 - Description: string { get; }
 - Principle.When(
     preCondition: bool
   ) : Precondition [MustUseReturnValue]
 - Principle.Requires(
     condition: bool
   ) : Requirement [MustUseReturnValue]
 - Throws(
     exception: Exception
   ) : Void

