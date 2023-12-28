# xunit.core 2.6.4.0

## Xunit.AssemblyTraitAttribute (attribute) : Attribute, ITraitAttribute
 - ctor(
     name: string,
     value: string
   )

## Xunit.ClassDataAttribute (attribute) : DataAttribute
 - Class: Type { get; }
 - Skip: string { get; set; }
 - ctor(
     class: Type
   )
 - GetData(
     testMethod: MethodInfo
   ) : IEnumerable<Object[]>

## Xunit.CollectionAttribute (attribute) : Attribute
 - ctor(
     name: string
   )

## Xunit.CollectionBehavior (enum) : IComparable, IFormattable, IConvertible
 - CollectionPerAssembly = 0
 - CollectionPerClass = 1

## Xunit.CollectionBehaviorAttribute (attribute) : Attribute
 - DisableTestParallelization: bool { get; set; }
 - MaxParallelThreads: int { get; set; }
 - ctor()
 - ctor(
     collectionBehavior: CollectionBehavior
   )
 - ctor(
     factoryTypeName: string,
     factoryAssemblyName: string
   )

## Xunit.CollectionDefinitionAttribute (attribute) : Attribute
 - DisableParallelization: bool { get; set; }
 - ctor(
     name: string
   )

## Xunit.Extensions.PropertyDataAttribute (attribute) : Attribute
 - PropertyType: Type { get; set; }
 - ctor(
     propertyName: string
   )

## Xunit.FactAttribute (attribute) : Attribute
 - DisplayName: string { get; set; }
 - Skip: string { get; set; }
 - Timeout: int { get; set; }
 - ctor()

## Xunit.IAsyncLifetime (interface)
 - DisposeAsync() : Task
 - InitializeAsync() : Task

## Xunit.IClassFixture<TFixture> (interface)

## Xunit.ICollectionFixture<TFixture> (interface)

## Xunit.InlineDataAttribute (attribute) : DataAttribute
 - Skip: string { get; set; }
 - ctor(
     data: params Object[] [ParamArray]
   )
 - GetData(
     testMethod: MethodInfo
   ) : IEnumerable<Object[]>

## Xunit.ITestCollectionOrderer (interface)
 - OrderTestCollections(
     testCollections: IEnumerable<ITestCollection>
   ) : IEnumerable<ITestCollection>

## Xunit.MemberDataAttribute (attribute) : MemberDataAttributeBase
 - DisableDiscoveryEnumeration: bool { get; set; }
 - MemberName: string { get; }
 - MemberType: Type { get; set; }
 - Parameters: Object[] { get; }
 - Skip: string { get; set; }
 - ctor(
     memberName: string,
     parameters: params Object[] [ParamArray]
   )
 - GetData(
     testMethod: MethodInfo
   ) : IEnumerable<Object[]>

## Xunit.MemberDataAttributeBase (abstract class) : DataAttribute
 - DisableDiscoveryEnumeration: bool { get; set; }
 - MemberName: string { get; }
 - MemberType: Type { get; set; }
 - Parameters: Object[] { get; }
 - Skip: string { get; set; }
 - GetData(
     testMethod: MethodInfo
   ) : IEnumerable<Object[]>

## Xunit.Record (class)
 - ctor()
 - Record.Exception(
     testCode: Action
   ) : Exception
 - Record.Exception(
     testCode: Func<object>
   ) : Exception
 - Record.Exception(
     testCode: Func<Task>
   ) : Exception [EditorBrowsable, Obsolete]
 - Record.ExceptionAsync(
     testCode: Func<Task>
   ) : Task<Exception> [AsyncStateMachine]

## Xunit.Sdk.AssemblyTraitDiscoverer (class) : ITraitDiscoverer
 - ctor()
 - GetTraits(
     traitAttribute: IAttributeInfo
   ) : IEnumerable<KeyValuePair<string, string>> [IteratorStateMachine]

## Xunit.Sdk.BeforeAfterTestAttribute (abstract class) : Attribute
 - After(
     methodUnderTest: MethodInfo
   ) : void
 - Before(
     methodUnderTest: MethodInfo
   ) : void

## Xunit.Sdk.DataAttribute (abstract class) : Attribute
 - Skip: string { get; set; }
 - GetData(
     testMethod: MethodInfo
   ) : IEnumerable<Object[]>

## Xunit.Sdk.DataDiscoverer (class) : IDataDiscoverer
 - ctor()
 - GetData(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : IEnumerable<Object[]>
 - SupportsDiscoveryEnumeration(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : bool

## Xunit.Sdk.DataDiscovererAttribute (attribute) : Attribute
 - ctor(
     typeName: string,
     assemblyName: string
   )

## Xunit.Sdk.ExceptionAggregator (class)
 - HasExceptions: bool { get; }
 - ctor()
 - ctor(
     parent: ExceptionAggregator
   )
 - Add(
     ex: Exception
   ) : void
 - Aggregate(
     aggregator: ExceptionAggregator
   ) : void
 - Clear() : void
 - Run(
     code: Action
   ) : void
 - RunAsync(
     code: Func<Task>
   ) : Task [AsyncStateMachine]
 - RunAsync<T>(
     code: Func<Task<T>>
   ) : Task<T> [AsyncStateMachine]
 - ToException() : Exception

## Xunit.Sdk.IDataDiscoverer (interface)
 - GetData(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : IEnumerable<Object[]>
 - SupportsDiscoveryEnumeration(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : bool

## Xunit.Sdk.IMessageBus (interface) : IDisposable
 - QueueMessage(
     message: IMessageSinkMessage
   ) : bool

## Xunit.Sdk.InlineDataDiscoverer (class) : IDataDiscoverer
 - ctor()
 - GetData(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : IEnumerable<Object[]>
 - SupportsDiscoveryEnumeration(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : bool

## Xunit.Sdk.ITestCaseOrderer (interface)
 - OrderTestCases<TTestCase>(
     testCases: IEnumerable<TTestCase>
   ) : IEnumerable<TTestCase>

## Xunit.Sdk.ITestFrameworkAttribute (interface)

## Xunit.Sdk.ITestFrameworkTypeDiscoverer (interface)
 - GetTestFrameworkType(
     attribute: IAttributeInfo
   ) : Type

## Xunit.Sdk.ITraitAttribute (interface)

## Xunit.Sdk.ITraitDiscoverer (interface)
 - GetTraits(
     traitAttribute: IAttributeInfo
   ) : IEnumerable<KeyValuePair<string, string>>

## Xunit.Sdk.IXunitTestCase (interface) : ITestCase, IXunitSerializable
 - InitializationException: Exception { get; }
 - Method: IMethodInfo { get; }
 - Timeout: int { get; }
 - RunAsync(
     diagnosticMessageSink: IMessageSink,
     messageBus: IMessageBus,
     constructorArguments: Object[],
     aggregator: ExceptionAggregator,
     cancellationTokenSource: CancellationTokenSource
   ) : Task<RunSummary>

## Xunit.Sdk.IXunitTestCaseDiscoverer (interface)
 - Discover(
     discoveryOptions: ITestFrameworkDiscoveryOptions,
     testMethod: ITestMethod,
     factAttribute: IAttributeInfo
   ) : IEnumerable<IXunitTestCase>

## Xunit.Sdk.IXunitTestCollectionFactory (interface)
 - DisplayName: string { get; }
 - Get(
     testClass: ITypeInfo
   ) : ITestCollection

## Xunit.Sdk.MemberDataDiscoverer (class) : DataDiscoverer, IDataDiscoverer
 - ctor()
 - GetData(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : IEnumerable<Object[]>
 - SupportsDiscoveryEnumeration(
     dataAttribute: IAttributeInfo,
     testMethod: IMethodInfo
   ) : bool

## Xunit.Sdk.PlatformSpecificAssemblyAttribute (attribute) : Attribute
 - ctor()

## Xunit.Sdk.RunSummary (class)
 - Total: int (field)
 - Failed: int (field)
 - Skipped: int (field)
 - Time: decimal (field)
 - ctor()
 - Aggregate(
     other: RunSummary
   ) : void

## Xunit.Sdk.TestFrameworkDiscovererAttribute (attribute) : Attribute
 - ctor(
     typeName: string,
     assemblyName: string
   )

## Xunit.Sdk.TestMethodDisplay (enum) : IComparable, IFormattable, IConvertible
 - ClassAndMethod = 1
 - Method = 2

## Xunit.Sdk.TestMethodDisplayOptions (enum) : IComparable, IFormattable, IConvertible
 - None = 0
 - ReplaceUnderscoreWithSpace = 1
 - UseOperatorMonikers = 2
 - UseEscapeSequences = 4
 - ReplacePeriodWithComma = 8
 - All = 15

## Xunit.Sdk.TraitDiscoverer (class) : ITraitDiscoverer
 - ctor()
 - GetTraits(
     traitAttribute: IAttributeInfo
   ) : IEnumerable<KeyValuePair<string, string>> [IteratorStateMachine]

## Xunit.Sdk.TraitDiscovererAttribute (attribute) : Attribute
 - ctor(
     typeName: string,
     assemblyName: string
   )

## Xunit.Sdk.XunitTestCaseDiscovererAttribute (attribute) : Attribute
 - ctor(
     typeName: string,
     assemblyName: string
   )

## Xunit.TestCaseOrdererAttribute (attribute) : Attribute
 - ctor(
     ordererTypeName: string,
     ordererAssemblyName: string
   )

## Xunit.TestCollectionOrdererAttribute (attribute) : Attribute
 - ctor(
     ordererTypeName: string,
     ordererAssemblyName: string
   )

## Xunit.TestFrameworkAttribute (attribute) : Attribute, ITestFrameworkAttribute
 - ctor(
     typeName: string,
     assemblyName: string
   )

## Xunit.TheoryAttribute (attribute) : FactAttribute
 - DisplayName: string { get; set; }
 - Skip: string { get; set; }
 - Timeout: int { get; set; }
 - ctor()

## Xunit.TheoryData (abstract class) : IEnumerable<Object[]>, IEnumerable
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p: T
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3,
     p4: T4,
     p5: T5,
     p6: T6,
     p7: T7,
     p8: T8,
     p9: T9,
     p10: T10
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3, T4> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3,
     p4: T4
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3, T4, T5> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3,
     p4: T4,
     p5: T5
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3, T4, T5, T6> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3,
     p4: T4,
     p5: T5,
     p6: T6
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3, T4, T5, T6, T7> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3,
     p4: T4,
     p5: T5,
     p6: T6,
     p7: T7
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3, T4, T5, T6, T7, T8> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3,
     p4: T4,
     p5: T5,
     p6: T6,
     p7: T7,
     p8: T8
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TheoryData<T1, T2, T3, T4, T5, T6, T7, T8, T9> (class) : TheoryData, IEnumerable<Object[]>, IEnumerable
 - ctor()
 - Add(
     p1: T1,
     p2: T2,
     p3: T3,
     p4: T4,
     p5: T5,
     p6: T6,
     p7: T7,
     p8: T8,
     p9: T9
   ) : void
 - GetEnumerator() : IEnumerator<Object[]>

## Xunit.TraitAttribute (attribute) : Attribute, ITraitAttribute
 - ctor(
     name: string,
     value: string
   )

