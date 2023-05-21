# Synergy.Web.Api.Testing

## HttpExtensions (abstract class)
 - HttpExtensions.GetAllHeaders(
     response: HttpResponseMessage
   ) : List<KeyValuePair<string, IEnumerable<string>>> [Extension]
 - HttpExtensions.GetAllRequestHeaders(
     operation: HttpOperation
   ) : List<KeyValuePair<string, IEnumerable<string>>> [Extension]
 - HttpExtensions.Read<T>(
     content: HttpContent? [NotNull],
     jsonPath: string [Nullable],
     value: out T& [Nullable, Out]
   ) : HttpContent? [NullableContext, Extension, MustUseReturnValue]
 - HttpExtensions.Read<T>(
     content: HttpContent? [NotNull]
   ) : T [NullableContext, Extension]
 - HttpExtensions.Read<T>(
     content: HttpContent? [Nullable, NotNull],
     jsonPath: string
   ) : T? [Extension, MustUseReturnValue]
 - HttpExtensions.ReadJson(
     content: HttpContent? [CanBeNull]
   ) : JToken? [NullableContext, Extension, MustUseReturnValue]
 - HttpExtensions.ToHttpLook(
     request: HttpRequestMessage,
     operation: HttpOperation
   ) : string [Extension]
 - HttpExtensions.ToHttpLook(
     response: HttpResponseMessage,
     operation: HttpOperation
   ) : string [Extension, NotNull]

## HttpOperation (class)
 - Description: string? [Nullable, CanBeNull] { get; }
 - Duration: TimeSpan { get; }
 - Request: HttpRequestMessage { get; }
 - Response: HttpResponseMessage { get; }
 - TestServer: TestServer { get; }
 - Assertions: List<IAssertion> (field)
 - ctor()

## HttpOperationExtensions (abstract class)
 - HttpOperationExtensions.Details<TOperation>(
     operation: TOperation,
     details: string
   ) : TOperation [Extension]
 - HttpOperationExtensions.ShouldBe<TOperation>(
     operation: TOperation,
     assertion: IAssertion
   ) : TOperation [Extension]
 - HttpOperationExtensions.ShouldBe<TOperation>(
     operation: TOperation,
     assertions: IEnumerable<IAssertion>
   ) : TOperation [Extension]

## TestServer (abstract class) : IDisposable
 - HttpClient: HttpClient [NotNull] { get; }
 - Repair: bool { get; set; }
 - SerializationSettings: JsonSerializerSettings { get; }
 - Delete(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : HttpOperation
 - Delete<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : TOperation
 - Dispose() : void
 - FailIfLeftInRepairMode() : void
 - Get(
     path: string,
     urlParameters: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : HttpOperation
 - Get<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : TOperation
 - Patch(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : HttpOperation
 - Patch<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : TOperation
 - Post(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : HttpOperation
 - Post<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : TOperation
 - Post(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: JToken [Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : HttpOperation
 - Put(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : HttpOperation
 - Put<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Action<HttpRequestHeaders>? [Nullable, Optional]
   ) : TOperation

## Json.Ignore (class)
 - Nodes: ReadOnlyCollection<string> { get; }
 - ctor(
     nodes: params String[] [ParamArray]
   )
 - And(
     ignore: Ignore
   ) : Ignore
 - Append(
     ignores: IEnumerable<string>
   ) : void
 - Ignore.RequestDescription() : Ignore
 - Ignore.RequestMethod() : Ignore
 - Ignore.ResponseBody(
     nodes: params String[] [ParamArray]
   ) : Ignore
 - Ignore.ResponseContentLength() : Ignore
 - Ignore.ResponseLocationHeader() : Ignore

## Json.JsonComparer (class)
 - AreEquivalent: bool { get; }
 - Ignore: Ignore { get; }
 - New: JToken { get; }
 - Pattern: JToken { get; }
 - ctor(
     pattern: JToken,
     new: JToken,
     ignore: Ignore? [Nullable, Optional]
   )
 - GetDifferences(
     maxNoOfDifferences: int [Optional]
   ) : string? [NullableContext]

## Features.Feature (class)
 - Scenarios: List<Scenario> { get; }
 - Title: string [NotNull] { get; }
 - ctor(
     title: string [NotNull]
   )
 - Scenario(
     title: string [NotNull]
   ) : Scenario [NotNull]

## Features.FeatureExtensions (abstract class)
 - FeatureExtensions.InStep<TOperation>(
     operation: TOperation [NotNull],
     step: Step [NotNull]
   ) : TOperation [NullableContext, Extension]

## Features.IExpectation (interface)
 - ExpectedResult: string { get; }

## Features.IHttpRequestStorage (interface)
 - GetSavedRequest() : HttpRequestMessage

## Features.IHttpResponseStorage (interface)
 - GetSavedResponse() : HttpResponseMessage

## Features.Markdown (class)
 - ctor(
     feature: Feature
   )
 - GenerateReportTo(
     filePath: string? [Nullable, Optional]
   ) : string

## Features.Scenario (class)
 - No: int { get; }
 - Steps: List<Step> { get; }
 - Title: string [NotNull] { get; }
 - ctor(
     title: string [NotNull],
     no: int
   )
 - Step(
     title: string [NotNull]
   ) : Step [NotNull]

## Features.Step (class)
 - Content: string? [Nullable] { get; }
 - No: int { get; }
 - Title: string { get; }
 - Operations: List<HttpOperation> (field)
 - ctor(
     title: string,
     no: int
   )
 - Markdown(
     markdown: string
   ) : void

## Assertions.Assertion (abstract class) : IAssertion, IExpectation
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result

## Assertions.IAssertion (interface) : IExpectation
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result [NullableContext]

## Assertions.CompareOperationWithPattern (class) : Assertion, IAssertion, IExpectation, IHttpRequestStorage, IHttpResponseStorage
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - ctor(
     patternFilePath: string,
     ignore: Ignore? [Nullable, Optional]
   )
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result
 - Ignore(
     ignore: string,
     ignores: params String[] [ParamArray]
   ) : CompareOperationWithPattern
 - Ignore(
     ignore: Ignore
   ) : CompareOperationWithPattern

## Assertions.CompareResponseWithPattern (class) : Assertion, IAssertion, IExpectation
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - ctor(
     patternFilePath: string,
     ignore: Ignore? [Nullable, Optional],
     mode: CompareResponseWithPattern+Mode [Optional]
   )
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result

## Assertions.VerifyRequestMethod (class) : Assertion, IAssertion, IExpectation
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - ctor(
     expectedMethod: params HttpMethod[] [ParamArray]
   )
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result

## Assertions.VerifyResponseBody (class) : Assertion, IAssertion, IExpectation
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - ctor(
     jsonToken: string,
     validate: Func<HttpOperation, JToken, Assertion+Result> [Nullable]
   )
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result

## Assertions.VerifyResponseContentType (class) : Assertion, IAssertion, IExpectation
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - ctor(
     expectedContentType: string
   )
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result

## Assertions.VerifyResponseHeader (class) : Assertion, IAssertion, IExpectation
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - ctor(
     headerName: string,
     validate: Func<HttpOperation, string, Assertion+Result>
   )
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result

## Assertions.VerifyResponseStatus (class) : Assertion, IAssertion, IExpectation
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Assertion+Result { get; }
 - ctor(
     expectedStatus: HttpStatusCode
   )
 - Assert(
     operation: HttpOperation
   ) : Assertion+Result [NullableContext]
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Assertion+Result

## Assertions.WebApiRuleViolationException (exception) : Exception, ISerializable
 - ctor(
     result: Assertion+Result
   )

## Assertions.Assertion+Result (struct)
 - IsOk: bool { get; }
 - Message: string { get; }
 - Result.Ok: Assertion+Result { get; }
 - ctor(
     message: string [NotNull]
   )

## Assertions.CompareResponseWithPattern+Mode (enum) : IComparable, IFormattable, IConvertible
 - ContractCheck = 1
 - Default = 2

