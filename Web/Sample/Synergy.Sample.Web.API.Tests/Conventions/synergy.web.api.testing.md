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
      request: HttpRequestMessage
   ) : List`1 [Extension]
 - HttpExtensions.GetAllHeaders(
      response: HttpResponseMessage
   ) : List`1 [Extension]
 - HttpExtensions.ToHttpLook(
      request: HttpRequestMessage
   ) : string [Extension, NotNull]
 - HttpExtensions.ToHttpLook(
      response: HttpResponseMessage
   ) : string [Extension, NotNull]

## HttpOperation
 - Description: string? [Nullable, CanBeNull]
 - Duration: TimeSpan
 - TestServer: TestServer
 - Request: HttpRequestMessage
 - Response: HttpResponseMessage
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
 - Repair: bool
 - FailIfLeftInRepairMode() : Void
 - Get(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional]
   ) : HttpOperation
 - Get<TOperation>(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional]
   ) : TOperation
 - Post(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional],
      body: object? [Nullable, Optional]
   ) : HttpOperation
 - Post<TOperation>(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional],
      body: object? [Nullable, Optional]
   ) : TOperation
 - Post(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional],
      body: JToken [Optional]
   ) : HttpOperation
 - Put(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional],
      body: object? [Nullable, Optional]
   ) : HttpOperation
 - Put<TOperation>(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional],
      body: object? [Nullable, Optional]
   ) : TOperation
 - Patch(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional],
      body: object? [Nullable, Optional]
   ) : HttpOperation
 - Patch<TOperation>(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional],
      body: object? [Nullable, Optional]
   ) : TOperation
 - Delete(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional]
   ) : HttpOperation
 - Delete<TOperation>(
      path: string,
      urlParameters: object? [Nullable, CanBeNull, Optional]
   ) : TOperation
 - Dispose() : Void

## Json.Ignore
 - Nodes: ReadOnlyCollection`1
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
 - Pattern: JToken
 - New: JToken
 - Ignore: Ignore
 - AreEquivalent: bool
 - GetDifferences(
      maxNoOfDifferences: int [Optional]
   ) : string? [NullableContext]

## Features.Feature
 - Title: string [NotNull]
 - Scenarios: List`1
 - Scenario(
      title: string [NotNull]
   ) : Scenario [NotNull]

## Features.FeatureExtensions
 - FeatureExtensions.InStep<TOperation>(
      operation: TOperation [NotNull],
      step: Step [NotNull]
   ) : TOperation? [NullableContext, Extension]

## Features.IExpectation
 - ExpectedResult: string

## Features.IHttpRequestStorage
 - GetSavedRequest() : HttpRequestMessage

## Features.IHttpResponseStorage
 - GetSavedResponse() : HttpResponseMessage

## Features.Markdown
 - GenerateReportTo(
      filePath: string? [Nullable, Optional]
   ) : string

## Features.Scenario
 - No: int
 - Title: string [NotNull]
 - Steps: List`1
 - Step(
      title: string [NotNull]
   ) : Step [NotNull]

## Features.Step
 - No: int
 - Title: string
 - Operations: List`1 (field)

## Assertions.Assertion
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - ExpectedResult: string? [Nullable]
 - Assertion.Ok: Result
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
 - TargetSite: MethodBase
 - StackTrace: string
 - Message: string? [Nullable]
 - Data: IDictionary? [Nullable]
 - InnerException: Exception
 - HelpLink: string
 - Source: string
 - HResult: int

## Assertions.Assertion+Result (struct)
 - Message: string
 - IsOk: bool
 - Result.Ok: Result

## Assertions.CompareResponseWithPattern+Mode (enum)
 - Mode.ContractCheck: Mode (field)
 - Mode.Default: Mode (field)

