# Synergy.Web.Api.Testing

## HttpExtensions:
 - ReadJson(
      content: HttpContent
   ) : JToken
 - Read(
      [NotNull] content: HttpContent,
      [Nullable] jsonPath: string,
      [Nullable, Out] value: T&
   ) : HttpContent
 - Read(
      [NotNull] content: HttpContent
   ) : T
 - Read(
      [Nullable, NotNull] content: HttpContent,
      jsonPath: string
   ) : T
 - GetRequestFullMethod(
      request: HttpRequestMessage
   ) : string
 - GetRequestRelativeUrl(
      request: HttpRequestMessage
   ) : string
 - GetAllHeaders(
      request: HttpRequestMessage
   ) : List`1
 - GetAllHeaders(
      response: HttpResponseMessage
   ) : List`1
 - ToHttpLook(
      request: HttpRequestMessage
   ) : string
 - ToHttpLook(
      response: HttpResponseMessage
   ) : string

## HttpOperation:
 - [Nullable, CanBeNull] Description: string?
 - Duration: TimeSpan
 - TestServer: TestServer
 - Request: HttpRequestMessage
 - Response: HttpResponseMessage

## HttpOperationExtensions:
 - Details(
      operation: TOperation,
      details: string
   ) : TOperation
 - ShouldBe(
      operation: TOperation,
      assertion: IAssertion
   ) : TOperation
 - ShouldBe(
      operation: TOperation,
      assertions: IEnumerable`1
   ) : TOperation

## TestServer:
 - HttpClient: HttpClient
 - Repair: bool
 - FailIfLeftInRepairMode() : Void
 - Get(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object
   ) : HttpOperation
 - Get(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object
   ) : TOperation
 - Post(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object,
      [Nullable, Optional] body: Object
   ) : HttpOperation
 - Post(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object,
      [Nullable, Optional] body: Object
   ) : TOperation
 - Post(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object,
      [Optional] body: JToken
   ) : HttpOperation
 - Put(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object,
      [Nullable, Optional] body: Object
   ) : HttpOperation
 - Put(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object,
      [Nullable, Optional] body: Object
   ) : TOperation
 - Patch(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object,
      [Nullable, Optional] body: Object
   ) : HttpOperation
 - Patch(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object,
      [Nullable, Optional] body: Object
   ) : TOperation
 - Delete(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object
   ) : HttpOperation
 - Delete(
      path: string,
      [Nullable, CanBeNull, Optional] urlParameters: Object
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
      [ParamArray] nodes: String[]
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
      [Optional] maxNoOfDifferences: int
   ) : string

## Features.Feature:
 - [NotNull] Title: string
 - Scenarios: List`1
 - Scenario(
      [NotNull] title: string
   ) : Scenario

## Features.FeatureExtensions:
 - InStep(
      [NotNull] operation: TOperation,
      [NotNull] step: Step
   ) : TOperation

## Features.IExpectation:
 - ExpectedResult: string

## Features.IHttpRequestStorage:
 - GetSavedRequest() : HttpRequestMessage

## Features.IHttpResponseStorage:
 - GetSavedResponse() : HttpResponseMessage

## Features.Markdown:
 - GenerateReportTo(
      [Nullable, Optional] filePath: string
   ) : string

## Features.Scenario:
 - No: int
 - [NotNull] Title: string
 - Steps: List`1
 - Step(
      [NotNull] title: string
   ) : Step

## Features.Step:
 - No: int
 - Title: string

## Assertions.Assertion:
 - [Nullable] ExpectedResult: string?
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
   ) : Result

## Assertions.CompareOperationWithPattern:
 - [Nullable] ExpectedResult: string?
 - Ok: Result
 - Assert(
      operation: HttpOperation
   ) : Result
 - Ignore(
      ignore: string,
      [ParamArray] ignores: String[]
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
 - [Nullable] ExpectedResult: string?
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
 - [Nullable] ExpectedResult: string?
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
 - [Nullable] ExpectedResult: string?
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
 - [Nullable] ExpectedResult: string?
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
 - [Nullable] ExpectedResult: string?
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
 - [Nullable] ExpectedResult: string?
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

## Assertions.WebApiRuleViolationException:
 - TargetSite: MethodBase
 - StackTrace: string
 - [Nullable] Message: string?
 - [Nullable] Data: IDictionary?
 - InnerException: Exception
 - HelpLink: string
 - Source: string
 - HResult: int

