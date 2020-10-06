# Synergy.Web.Api.Testing

## HttpExtensions:
 - ReadJson(
      content: HttpContent [CanBeNull]
   ) : JToken? [NullableContext, Extension, MustUseReturnValue]
 - Read<T>(
      content: HttpContent [NotNull],
      jsonPath: string? [Nullable],
      value: T&? [Nullable, Out]
   ) : HttpContent? [NullableContext, Extension, MustUseReturnValue]
 - Read<T>(
      content: HttpContent [NotNull]
   ) : T? [NullableContext, Extension]
 - Read<T>(
      content: HttpContent? [Nullable, NotNull],
      jsonPath: string
   ) : T [Extension, MustUseReturnValue]
 - GetRequestFullMethod(
      request: HttpRequestMessage
   ) : string [Extension, Pure]
 - GetRequestRelativeUrl(
      request: HttpRequestMessage
   ) : string [Extension, Pure]
 - GetAllHeaders(
      request: HttpRequestMessage
   ) : List`1 [Extension]
 - GetAllHeaders(
      response: HttpResponseMessage
   ) : List`1 [Extension]
 - ToHttpLook(
      request: HttpRequestMessage
   ) : string [Extension, NotNull]
 - ToHttpLook(
      response: HttpResponseMessage
   ) : string [Extension, NotNull]

## HttpOperation:
 - Description: string? [Nullable, CanBeNull]
 - Duration: TimeSpan
 - TestServer: TestServer
 - Request: HttpRequestMessage
 - Response: HttpResponseMessage

## HttpOperationExtensions:
 - Details<TOperation>(
      operation: TOperation,
      details: string
   ) : TOperation [Extension]
 - ShouldBe<TOperation>(
      operation: TOperation,
      assertion: IAssertion
   ) : TOperation [Extension]
 - ShouldBe<TOperation>(
      operation: TOperation,
      assertions: IEnumerable`1
   ) : TOperation [Extension]

## TestServer:
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

## Json.Ignore:
 - Nodes: ReadOnlyCollection`1
 - Append(
      ignores: IEnumerable`1
   ) : Void
 - And(
      ignore: Ignore
   ) : Ignore
 - ResponseBody(
      nodes: params String[] [ParamArray]
   ) : Ignore
 - RequestMethod() : Ignore
 - RequestDescription() : Ignore
 - ResponseLocationHeader() : Ignore
 - ResponseContentLength() : Ignore

## Json.JsonComparer:
 - Pattern: JToken
 - New: JToken
 - Ignore: Ignore
 - AreEquivalent: bool
 - GetDifferences(
      maxNoOfDifferences: int [Optional]
   ) : string? [NullableContext]

## Features.Feature:
 - Title: string [NotNull]
 - Scenarios: List`1
 - Scenario(
      title: string [NotNull]
   ) : Scenario [NotNull]

## Features.FeatureExtensions:
 - InStep<TOperation>(
      operation: TOperation [NotNull],
      step: Step [NotNull]
   ) : TOperation? [NullableContext, Extension]

## Features.IExpectation:
 - ExpectedResult: string

## Features.IHttpRequestStorage:
 - GetSavedRequest() : HttpRequestMessage

## Features.IHttpResponseStorage:
 - GetSavedResponse() : HttpResponseMessage

## Features.Markdown:
 - GenerateReportTo(
      filePath: string? [Nullable, Optional]
   ) : string

## Features.Scenario:
 - No: int
 - Title: string [NotNull]
 - Steps: List`1
 - Step(
      title: string [NotNull]
   ) : Step [NotNull]

## Features.Step:
 - No: int
 - Title: string

## Assertions.Assertion:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result
 - Expected(
      expected: string
   ) : IAssertion
 - Failure(
      message: string
   ) : Result

## Assertions.IAssertion:
 - Assert(
      operation: HttpOperation
   ) : Result? [NullableContext]

## Assertions.CompareOperationWithPattern:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
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
 - Failure(
      message: string
   ) : Result

## Assertions.CompareResponseWithPattern:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result
 - Expected(
      expected: string
   ) : IAssertion
 - Failure(
      message: string
   ) : Result

## Assertions.VerifyRequestMethod:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result
 - Expected(
      expected: string
   ) : IAssertion
 - Failure(
      message: string
   ) : Result

## Assertions.VerifyResponseBody:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result
 - Expected(
      expected: string
   ) : IAssertion
 - Failure(
      message: string
   ) : Result

## Assertions.VerifyResponseContentType:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result
 - Expected(
      expected: string
   ) : IAssertion
 - Failure(
      message: string
   ) : Result

## Assertions.VerifyResponseHeader:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result
 - Expected(
      expected: string
   ) : IAssertion
 - Failure(
      message: string
   ) : Result

## Assertions.VerifyResponseStatus:
 - ExpectedResult: string? [Nullable]
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result? [NullableContext]
 - Expected(
      expected: string
   ) : IAssertion
 - Failure(
      message: string
   ) : Result

## Assertions.WebApiRuleViolationException:
 - TargetSite: MethodBase
 - StackTrace: string
 - Message: string? [Nullable]
 - Data: IDictionary? [Nullable]
 - InnerException: Exception
 - HelpLink: string
 - Source: string
 - HResult: int

