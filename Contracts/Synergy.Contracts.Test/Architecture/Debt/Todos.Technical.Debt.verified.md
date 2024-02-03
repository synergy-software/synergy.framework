# Technical Debt for Synergy.Contracts

Total: 15

## [BusinessDocumentation.cs](../../Requirements/BusinessDocumentation.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: check that and probably convert docs int tt

## [Synergy.Contracts.Test.csproj](../../Synergy.Contracts.Test.csproj)
- TODO: Add net472;net48;net5.0;net6.0;net7.0;net8.0;netstandard2.0;netstandard2.1 or stop supporting old frameworks
- TODO: Remove NUnit and replace it with xunit

## [FailBoolean.cs](../../../Synergy.Contracts/Failures/FailBoolean.cs)
- TODO:mace (from:mace @ 22-10-2016): variable.FailIfFalse(message)
- TODO:mace (from:mace @ 22-10-2016): variable.FailIfTrue(message)

## [FailCast.cs](../../../Synergy.Contracts/Failures/FailCast.cs)
- TODO:mace (from:mace @ 22-10-2016): public static void Fail.IfArgumentNotCastable<T>([CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue)

## [FailCollection.cs](../../../Synergy.Contracts/Failures/FailCollection.cs)
- TODO:mace (from:mace @ 22-10-2016) public static void IfCollectionDoesNotContain<T>([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,)
- TODO: Marcin Celej [from: Marcin Celej on: 29-05-2023]: Fail.IfCollectionContainsDuplicates

## [FailEquality.cs](../../../Synergy.Contracts/Failures/FailEquality.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Add variable.OrFailIfEqual(sth)
- TODO:mace (from:mace @ 22-10-2016): a.FailIfEqual(b)
- TODO:mace (from:mace @ 22-10-2016): IfArgumentNotEqual
- TODO:mace (from:mace @ 22-10-2016): a.FailIfNotEqual(b)

## [FailGuid.cs](../../../Synergy.Contracts/Failures/FailGuid.cs)
- TODO:mace (from:mace @ 22-10-2016): guid.FailIfEmpty

## [FailNullability.cs](../../../Synergy.Contracts/Failures/FailNullability.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Use [CallerExpression] here - check https://andrewlock.net/exploring-dotnet-6-part-11-callerargumentexpression-and-throw-helpers/

## [Synergy.Contracts.csproj](../../../Synergy.Contracts/Synergy.Contracts.csproj)
- TODO: Add explicit versioning
