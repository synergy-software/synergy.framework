# Technical Debt for Synergy.Contracts

Total: 18

## [CreateUserCommandHandler.cs](../../Sample/Synergy.Sample.Web.API.Services/Users/Commands/CreateUser/CreateUserCommandHandler.cs)
- TODO: Add validation (bad-request) mechanism - maybe use data annotations?

## [ApiConventionFor.cs](../../Sample/Synergy.Sample.Web.API.Tests/Infrastructure/ApiConventionFor.cs)
- TODO: Add other ProblemDetails fields

## [Repair.cs](../../Sample/Synergy.Sample.Web.API.Tests/Repair.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Do something to prevent orphan process from this web api tests

## [ExceptionHandlingMiddleware.cs](../../Sample/Synergy.Sample.Web.API/Extensions/ExceptionHandlingMiddleware.cs)
- TODO: Add swagger filter that shows the possible error
- TODO: Log the error id so it would be esier to find the exception occurence when someone sends the id
- TODO: How to log correlationId
- TODO: Depending on exception type decide to return (or not) exception details to the client
- TODO: Rozważ zwracanie struktury ProblemDetails - zgodnie z https://tools.ietf.org/html/rfc7807

## [EnvironmentLogProperties.cs](../../Sample/Synergy.Sample.Web.API/Extensions/Logging/EnvironmentLogProperties.cs)
- TODO: Logging: consider adding explicit EventId to each event
- TODO: Add dedicated components for logging

## [SwaggerExtensions.cs](../../Sample/Synergy.Sample.Web.API/Extensions/SwaggerExtensions.cs)
- TODO: Dodaj filtry

## [Program.cs](../../Sample/Synergy.Sample.Web.API/Program.cs)
- TODO: Move the serilog configuration deeper - so it could be different for every env

## [Startup.cs](../../Sample/Synergy.Sample.Web.API/Startup.cs)
- TODO: Additionally log (trace/debug level) full request and response - with dedicated middleware/filter
- TODO: Move the EnvironmentName to Serilog global config - after you move it deeper from Main()

## [CompareOperationWithPattern.cs](../../Synergy.Web.Api.Testing/Assertions/CompareOperationWithPattern.cs)
- TODO: Add assertion results

## [CompareResponseWithPattern.cs](../../Synergy.Web.Api.Testing/Assertions/CompareResponseWithPattern.cs)
- TODO: Add non-nullable annotations to OrFail() - and other contract methods

## [Markdown.cs](../../Synergy.Web.Api.Testing/Features/Markdown.cs)
- TODO:Marcin Celej: Do not regenerate report if every operation was 'unchanged' (check somehow HttpOperation.Assertions) - to achieve this add Status to HttpOperation

## [GenerateApiDescription.cs](../Conventions/GenerateApiDescription.cs)
- TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: enable this test
