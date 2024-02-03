# Synergy.Contracts

## DesignByContractViolationException (exception) : Exception, ISerializable
 - ctor()
 - ctor(
     message: string [NotNull, NotNull]
   )

## Fail (abstract class)
 - Fail.AsOrFail<T>(
     value: object [CanBeNull, NoEnumeration],
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T? [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.Because(
     message: string [NotNull, NotNull]
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because(
     message: string [NotNull, NotNull],
     args: params Object[] [ParamArray, NotNull, NotNull]
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because(
     message: Violation
   ) : DesignByContractViolationException [NotNull, Pure]
 - Fail.Because<T1, T2, T3>(
     message: string [NotNull, NotNull],
     arg1: T1?,
     arg2: T2?,
     arg3: T3?
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1, T2>(
     message: string [NotNull, NotNull],
     arg1: T1?,
     arg2: T2?
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.Because<T1>(
     message: string [NotNull, NotNull],
     arg1: T1?
   ) : DesignByContractViolationException [NotNull, Pure, StringFormatMethod]
 - Fail.BecauseEnumOutOfRange<T>(
     value: T
   ) : DesignByContractViolationException [NullableContext, NotNull, Pure]
 - Fail.CanBeNull<T>(
     value: T? [CanBeNull, NoEnumeration]
   ) : T? [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.CastEnumOrFail<T>(
     value: Enum [CanBeNull, NoEnumeration],
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T? [Extension, NotNull]
 - Fail.CastOrFail<T>(
     value: object [CanBeNull, NoEnumeration],
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T? [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.FailIfEmpty(
     value: DateTime,
     name: string? [CallerArgumentExpression, Optional]
   ) : DateTime [NullableContext, Extension, AssertionMethod]
 - Fail.FailIfEnumOutOfRange<T>(
     value: Enum,
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T? [Extension, NotNull]
 - Fail.FailIfNotDate(
     date: DateTime,
     name: string? [CallerArgumentExpression, Optional]
   ) : DateTime [NullableContext, Extension, AssertionMethod]
 - Fail.FailIfNotDate(
     date: DateTime? [CanBeNull],
     name: string? [CallerArgumentExpression, Optional]
   ) : DateTime? [NullableContext, Extension, CanBeNull, AssertionMethod]
 - Fail.FailIfNull<T>(
     value: T? [CanBeNull, AssertionCondition, NoEnumeration],
     message: Violation
   ) : T? [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.FailIfNull<T>(
     value: T? [CanBeNull, AssertionCondition, NoEnumeration],
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T? [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentEmpty(
     argumentValue: string [CanBeNull, AssertionCondition],
     argumentName: string [NotNull, NotNull]
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentEmpty(
     value: Guid,
     argumentName: string [NotNull]
   ) : void [AssertionMethod]
 - Fail.IfArgumentEqual<TExpected, TActual>(
     unexpected: TExpected [Nullable, CanBeNull],
     argumentValue: TActual [Nullable, CanBeNull],
     argumentName: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod]
 - Fail.IfArgumentNull<T>(
     argumentValue: T [Nullable, CanBeNull, AssertionCondition, NoEnumeration],
     argumentName: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod, ContractAnnotation]
 - Fail.IfArgumentWhiteSpace(
     argumentValue: string [CanBeNull, AssertionCondition],
     argumentName: string [NotNull, NotNull]
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionContains<T>(
     collection: IEnumerable<T> [CanBeNull, AssertionCondition],
     func: Func<T, bool> [NotNull, NotNull],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionContainsNull<T>(
     collection: IEnumerable<T> [CanBeNull, AssertionCondition],
     collectionName: string? [Nullable, CallerArgumentExpression, Optional]
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionEmpty(
     collection: IEnumerable [CanBeNull, AssertionCondition],
     collectionName: string? [Nullable, CallerArgumentExpression, Optional]
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionEmpty(
     collection: IEnumerable [CanBeNull, AssertionCondition],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfCollectionsAreNotEquivalent<T>(
     collection1: IEnumerable<T> [CanBeNull, AssertionCondition],
     collection2: IEnumerable<T> [CanBeNull, AssertionCondition],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfEmpty(
     value: DateTime,
     name: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod]
 - Fail.IfEmpty(
     value: Guid,
     message: Violation
   ) : void [AssertionMethod]
 - Fail.IfEmpty(
     value: string [CanBeNull, AssertionCondition],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfEmpty(
     value: string [CanBeNull, AssertionCondition],
     name: string [NotNull, NotNull]
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfEnumNotDefined<T>(
     value: object [NotNull, NotNull]
   ) : void
 - Fail.IfEnumNotDefined<T>(
     value: T
   ) : void [NullableContext]
 - Fail.IfEqual<TExpected, TActual>(
     unexpected: TExpected [Nullable, CanBeNull],
     actual: TActual [Nullable, CanBeNull],
     name: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod]
 - Fail.IfEqual<TExpected, TActual>(
     unexpected: TExpected? [CanBeNull],
     actual: TActual? [CanBeNull],
     message: Violation
   ) : void [AssertionMethod]
 - Fail.IfFalse(
     value: bool [AssertionCondition],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfNotCastable(
     value: object [CanBeNull, NoEnumeration],
     expectedType: Type [NotNull, NotNull],
     message: Violation
   ) : void [AssertionMethod]
 - Fail.IfNotCastable<T>(
     value: object [CanBeNull, NoEnumeration],
     message: Violation
   ) : void [AssertionMethod]
 - Fail.IfNotDate(
     date: DateTime? [CanBeNull],
     message: Violation
   ) : void [AssertionMethod]
 - Fail.IfNotDate(
     date: DateTime?,
     name: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod]
 - Fail.IfNotEqual<TExpected, TActual>(
     expected: TExpected [Nullable, CanBeNull],
     actual: TActual [Nullable, CanBeNull],
     name: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod]
 - Fail.IfNotEqual<TExpected, TActual>(
     expected: TExpected? [CanBeNull],
     actual: TActual? [CanBeNull],
     message: Violation
   ) : void [AssertionMethod]
 - Fail.IfNotNull<T>(
     value: T [Nullable, CanBeNull, NoEnumeration],
     name: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod, ContractAnnotation]
 - Fail.IfNotNull<T>(
     value: T? [CanBeNull, NoEnumeration],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfNull<T>(
     value: T [Nullable, CanBeNull, AssertionCondition],
     name: string? [CallerArgumentExpression, Optional]
   ) : void [NullableContext, AssertionMethod, ContractAnnotation]
 - Fail.IfNull<T>(
     value: T? [CanBeNull, AssertionCondition],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfNullOrNotCastable<T>(
     value: object [CanBeNull, NoEnumeration]
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfNullOrNotCastable<T>(
     value: object [CanBeNull, NoEnumeration],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfTooLong(
     value: string [CanBeNull],
     maxLength: int,
     name: string [NotNull, NotNull]
   ) : void [AssertionMethod]
 - Fail.IfTooLongOrWhitespace(
     value: string [CanBeNull],
     maxLength: int,
     name: string [NotNull, NotNull]
   ) : void [AssertionMethod]
 - Fail.IfTrue(
     value: bool [AssertionCondition],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfWhitespace(
     value: string [CanBeNull, AssertionCondition],
     message: Violation
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.IfWhitespace(
     value: string [CanBeNull, AssertionCondition],
     name: string [NotNull, NotNull]
   ) : void [AssertionMethod, ContractAnnotation]
 - Fail.NotNull<T>(
     value: T? [CanBeNull, AssertionCondition, NoEnumeration],
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T? [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFail<T>(
     value: T? [CanBeNull, AssertionCondition, NoEnumeration],
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T? [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFail<T>(
     value: T?,
     name: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T [NullableContext, Extension, ContractAnnotation]
 - Fail.OrFailIfCollectionEmpty<T>(
     collection: T [CanBeNull, AssertionCondition],
     collectionName: string? [Nullable, CallerArgumentExpression, Optional]
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfCollectionEmpty<T>(
     collection: T [CanBeNull, AssertionCondition],
     message: Violation
   ) : T [Extension, NotNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfTooLong(
     value: string [CanBeNull],
     maxLength: int,
     name: string [NotNull, NotNull]
   ) : string [Extension, CanBeNull, AssertionMethod, ContractAnnotation]
 - Fail.OrFailIfWhiteSpace(
     value: string? [Nullable, AssertionCondition, NoEnumeration],
     name: string [NotNull, NotNull, CallerArgumentExpression, Optional]
   ) : string [Extension, NotNull, AssertionMethod, ContractAnnotation]

## Requirements.Business (abstract class)
 - Business.Requires(
     condition: bool
   ) : Business+Requirement [MustUseReturnValue]
 - Business.Rule(
     description: string
   ) : Business+Principle [MustUseReturnValue]
 - Business.When(
     preCondition: bool,
     expression: string? [CallerArgumentExpression, Optional]
   ) : Business+Precondition [NullableContext, MustUseReturnValue]

## Requirements.Business+IPrecondition (interface)
 - Met: bool { get; }

## Requirements.Business+Precondition (struct) : Business+IPrecondition
 - Comment: string [CanBeNull] { get; }
 - Item: Business+Precondition { get; }
 - Met: bool { get; }
 - ctor(
     preCondition: bool,
     previous: Business+IPrecondition [CanBeNull, Optional],
     comment: string [CanBeNull, Optional]
   )
 - And(
     preCondition: bool
   ) : Business+Precondition [MustUseReturnValue]
 - Requires(
     condition: bool
   ) : Business+Requirement [MustUseReturnValue]
 - Requires(
     condition: Func<bool>
   ) : Business+Requirement [MustUseReturnValue]

## Requirements.Business+Principle (struct)
 - Description: string { get; }
 - ctor(
     description: string
   )
 - Requires(
     condition: bool
   ) : Business+Requirement [MustUseReturnValue]
 - Throws(
     exception: Exception
   ) : void
 - When(
     preCondition: bool
   ) : Business+Precondition [MustUseReturnValue]

## Requirements.Business+Requirement (struct)
 - Comment: string [CanBeNull] { get; }
 - Item: Business+Requirement { get; }
 - Met: bool { get; }
 - ctor(
     condition: bool
   )
 - ctor(
     precondition: Business+Precondition?,
     condition: bool
   )
 - ctor(
     precondition: Business+Precondition?,
     condition: Func<bool>,
     comment: string [CanBeNull, Optional]
   )
 - Throws(
     exception: Exception
   ) : void
 - Throws(
     message: string
   ) : void

## Requirements.BusinessRuleViolationException (exception) : Exception, ISerializable
 - Requirement: Business+Requirement { get; }
 - ctor(
     message: string,
     requirement: Business+Requirement
   )

## Violation (struct)
 - ctor(
     message: string [NotNull, NotNull],
     args: params Object[] [ParamArray, NotNull, NotNull]
   )
 - Violation.Of(
     message: string [NotNull, NotNull],
     args: params Object[] [ParamArray, NotNull, NotNull]
   ) : Violation [StringFormatMethod]

