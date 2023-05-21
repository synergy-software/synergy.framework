# Technical Debt for Synergy.Contracts

Total: 14

## [ContractorRepository.cs](../../../Synergy.Contracts.Samples/ContractorRepository.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 30-04-2023]: Remove this sample project

## [BusinessDocumentation.cs](../../Requirements/BusinessDocumentation.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: check that and probably convert docs int tt

## [!Init.cs](../../../Synergy.Contracts/!Init.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 14-05-2023]: Stop supporting netcoreapp2.1;netcoreapp3.1;

## [FailBoolean.cs](../../../Synergy.Contracts/Failures/FailBoolean.cs)
- TODO:mace (from:mace @ 22-10-2016): variable.FailIfFalse(message)
- TODO:mace (from:mace @ 22-10-2016): variable.FailIfTrue(message)

## [FailCast.cs](../../../Synergy.Contracts/Failures/FailCast.cs)
- TODO:mace (from:mace @ 22-10-2016): Add [AssertionCondition] below
- TODO:mace (from:mace @ 22-10-2016): public static void Fail.IfArgumentNotCastable<T>([CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue)

## [FailCollection.cs](../../../Synergy.Contracts/Failures/FailCollection.cs)
- TODO:mace (from:mace @ 22-10-2016) public static void IfCollectionDoesNotContain<T>([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,)

## [FailEquality.cs](../../../Synergy.Contracts/Failures/FailEquality.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Add variable.OrFailIfEqual(sth)
- TODO:mace (from:mace @ 22-10-2016): a.FailIfEqual(b)
- TODO:mace (from:mace @ 22-10-2016): IfArgumentNotEqual
- TODO:mace (from:mace @ 22-10-2016): a.FailIfNotEqual(b)

## [FailGuid.cs](../../../Synergy.Contracts/Failures/FailGuid.cs)
- TODO:mace (from:mace @ 22-10-2016): guid.FailIfEmpty

## [FailNullability.cs](../../../Synergy.Contracts/Failures/FailNullability.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Use [CallerExpression] here - check https://andrewlock.net/exploring-dotnet-6-part-11-callerargumentexpression-and-throw-helpers/
