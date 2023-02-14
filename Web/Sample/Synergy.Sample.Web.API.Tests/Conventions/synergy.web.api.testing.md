# Synergy.Web.Api.Testing

## HttpExtensions
 - HttpExtensions.ReadJson(
     content: HttpContent [CanBeNull]
   ) : JToken? [NullableContext, Extension, MustUseReturnValue]
 - HttpExtensions.Read<T>(
     content: HttpContent [NotNull],
     jsonPath: string? [Nullable],
     value: out T&? [Nullable, Out]
   ) : HttpContent? [NullableContext, Extension, MustUseReturnValue]
 - HttpExtensions.Read<T>(
     content: HttpContent [NotNull]
   ) : T? [NullableContext, Extension]
 - HttpExtensions.Read<T>(
     content: HttpContent? [Nullable, NotNull],
     jsonPath: string
   ) : T [Extension, MustUseReturnValue]
 - HttpExtensions.GetAllHeaders(
     request: HttpRequestMessage,
     httpClientDefaultRequestHeaders: HttpRequestHeaders
   ) : List`1 [Extension]
 - HttpExtensions.GetAllHeaders(
     response: HttpResponseMessage
   ) : List`1 [Extension]
 - HttpExtensions.ToHttpLook(
     request: HttpRequestMessage,
     operation: HttpOperation
   ) : string [Extension]
 - HttpExtensions.ToHttpLook(
     response: HttpResponseMessage,
     operation: HttpOperation
   ) : string [Extension, NotNull]

## HttpOperation
 - Description: string? [Nullable, CanBeNull] { get; }
 - Duration: TimeSpan { get; }
 - TestServer: TestServer { get; }
 - Request: HttpRequestMessage { get; }
 - Response: HttpResponseMessage { get; }
 - Assertions: List`1 (field)

## HttpOperationExtensions
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
     assertions: IEnumerable`1
   ) : TOperation [Extension]

## TestServer
 - HttpClient: HttpClient [NotNull] { get; }
 - Repair: bool { get; set; }
 - SerializationSettings: JsonSerializerSettings { get; }
 - FailIfLeftInRepairMode() : Void
 - Get(
     path: string,
     urlParameters: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : HttpOperation
 - Get<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : TOperation
 - Post(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : HttpOperation
 - Post<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : TOperation
 - Post(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: JToken [Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : HttpOperation
 - Put(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : HttpOperation
 - Put<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : TOperation
 - Patch(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : HttpOperation
 - Patch<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : TOperation
 - Delete(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : HttpOperation
 - Delete<TOperation>(
     path: string,
     urlParameters: object? [Nullable, Optional],
     body: object? [Nullable, Optional],
     headers: Dictionary`2? [Nullable, Optional]
   ) : TOperation
 - Dispose() : Void

## Json.Ignore
 - Nodes: ReadOnlyCollection`1 { get; }
 - Append(
     ignores: IEnumerable`1
   ) : Void
 - And(
     ignore: Ignore
   ) : Ignore
 - Ignore.ResponseBody(
     nodes: params String[] [ParamArray]
   ) : Ignore
 - Ignore.RequestMethod() : Ignore
 - Ignore.RequestDescription() : Ignore
 - Ignore.ResponseLocationHeader() : Ignore
 - Ignore.ResponseContentLength() : Ignore

## Json.JsonComparer
 - Pattern: JToken { get; }
 - New: JToken { get; }
 - Ignore: Ignore { get; }
 - AreEquivalent: bool { get; }
 - GetDifferences(
     maxNoOfDifferences: int [Optional]
   ) : string? [NullableContext]

## Features.Feature
 - Title: string [NotNull] { get; }
 - Scenarios: List`1 { get; }
 - Scenario(
     title: string [NotNull]
   ) : Scenario [NotNull]

## Features.FeatureExtensions
 - FeatureExtensions.InStep<TOperation>(
     operation: TOperation [NotNull],
     step: Step [NotNull]
   ) : TOperation? [NullableContext, Extension]

## Features.IExpectation
 - ExpectedResult: string { get; }

## Features.IHttpRequestStorage
 - GetSavedRequest() : HttpRequestMessage

## Features.IHttpResponseStorage
 - GetSavedResponse() : HttpResponseMessage

## Features.Markdown
 - GenerateReportTo(
     filePath: string? [Nullable, Optional]
   ) : string

## Features.Scenario
 - No: int { get; }
 - Title: string [NotNull] { get; }
 - Steps: List`1 { get; }
 - Step(
     title: string [NotNull]
   ) : Step [NotNull]

## Features.Step
 - No: int { get; }
 - Title: string { get; }
 - Operations: List`1 (field)

## Assertions.Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.IAssertion
 - Assert(
     operation: HttpOperation
   ) : Result? [NullableContext]

## Assertions.CompareOperationWithPattern : Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result
 - Ignore(
     ignore: string,
     ignores: params String[] [ParamArray]
   ) : CompareOperationWithPattern
 - Ignore(
     ignore: Ignore
   ) : CompareOperationWithPattern
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.CompareResponseWithPattern : Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.VerifyRequestMethod : Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.VerifyResponseBody : Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.VerifyResponseContentType : Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.VerifyResponseHeader : Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.VerifyResponseStatus : Assertion
 - ExpectedResult: string? [Nullable] { get; }
 - Assertion.Ok: Result { get; }
 - Assert(
     operation: HttpOperation
   ) : Result? [NullableContext]
 - Expected(
     expected: string
   ) : IAssertion
 - Assertion.Failure(
     message: string
   ) : Result

## Assertions.WebApiRuleViolationException : Exception
 - TargetSite: MethodBase { get; }
 - StackTrace: string { get; }
 - Message: string? [Nullable] { get; }
 - Data: IDictionary? [Nullable] { get; }
 - InnerException: Exception { get; }
 - HelpLink: string { get; set; }
 - Source: string { get; set; }
 - HResult: int { get; set; }

## Assertions.Assertion+Result (struct)
 - Message: string { get; }
 - IsOk: bool { get; }
 - Result.Ok: Result { get; }

## Assertions.CompareResponseWithPattern+Mode (enum)
 - Mode.ContractCheck: Mode (field)
 - Mode.Default: Mode (field)

