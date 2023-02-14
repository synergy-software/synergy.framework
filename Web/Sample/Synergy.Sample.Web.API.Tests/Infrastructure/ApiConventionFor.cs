using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using Newtonsoft.Json.Linq;
using Synergy.Sample.Web.API.Extensions;
using Synergy.Web.Api.Testing;
using Synergy.Web.Api.Testing.Assertions;

namespace Synergy.Sample.Web.API.Tests.Infrastructure
{
    public static class ApiConventionFor
    {
        private static IAssertion RequestMethodIs(HttpMethod method)
        {
            return new VerifyRequestMethod(method)
               .Expected($"Convention: HTTP request method is {method}");
        }

        private static IAssertion ResponseStatusIs(HttpStatusCode status)
        {
            return new VerifyResponseStatus(status)
               .Expected($"Convention: Returned HTTP status code is {(int)status} ({status})");
        }

        private static IEnumerable<IAssertion> CreationRequest()
        {
            yield return ApiConventionFor.RequestMethodIs(HttpMethod.Post);
        }

        /// <summary>
        /// Gets set of assertions that verify if creation operation meets convention.
        /// E.g. if created element is returned properly from Web API or if POST method is used, etc.
        /// </summary>
        public static IEnumerable<IAssertion> CreateResource()
        {
            // Request
            foreach (var assertion in ApiConventionFor.CreationRequest()) 
                yield return assertion;

            // Response
            yield return ApiConventionFor.ResponseStatusIs(HttpStatusCode.Created);

            yield return new VerifyResponseHeader(
                    "Location",
                    (operation, value)
                        =>
                    {
                        if (String.IsNullOrWhiteSpace(value) == false)
                            return Assertion.Ok;

                        return Assertion.Failure(
                            $"There is no 'Location' header returned in response:\n\n{operation.Response.ToHttpLook(operation)}"
                            );
                    })
               .Expected("Convention: Location header (pointing to newly created element) is returned with response.");

            yield return ApiConventionFor.ResponseContentTypeIs(MediaTypeNames.Application.Json);
        }

        public static IEnumerable<IAssertion> GetListOfResources()
        {
            // Request
            yield return ApiConventionFor.RequestMethodIs(HttpMethod.Get);

            // Response
            yield return ApiConventionFor.ResponseStatusIs(HttpStatusCode.OK);
            yield return ApiConventionFor.ResponseContentTypeIs(MediaTypeNames.Application.Json);
        }

        private static IAssertion ResponseContentTypeIs(string contentType)
        {
            return new VerifyResponseContentType(contentType)
               .Expected($"Convention: Returned HTTP Content-Type is \"{contentType}\"");
        }

        public static IEnumerable<IAssertion> CreateResourceWithValidationError()
        {
            foreach (var assertion in ApiConventionFor.CreationRequest().Concat(ApiConventionFor.BadRequest()))
            {
                yield return assertion;
            }

            yield return new VerifyResponseHeader(
                "Location",
                (operation, value)
                    =>
                {
                    if (String.IsNullOrWhiteSpace(value))
                        return Assertion.Ok;

                    return Assertion.Failure(
                        $"There is 'Location' header returned in response and it shouldn't be:\n\n{operation.Response.ToHttpLook(operation)}"
                        );
                }).Expected("Convention: There is NO \"Location\" header returned in response");
        }

        private static IEnumerable<IAssertion> BadRequest()
        {
            yield return ApiConventionFor.ResponseStatusIs(HttpStatusCode.BadRequest);

            yield return new VerifyResponseBody("title", (operation, token) => ApiConventionFor.ValidateIfNodeExists(operation, token, "title"))
               .Expected("Convention: error JSON contains \"title\" node");

            yield return new VerifyResponseBody("traceId", (operation, token) => ApiConventionFor.ValidateIfNodeExists(operation, token, "traceId"))
               .Expected("Convention: error JSON contains \"traceId\" node");
            
            // TODO: Add other ProblemDetails fields
        }

        private static Assertion.Result ValidateIfNodeExists(HttpOperation operation, JToken? token, string node)
        {
            if (token == null)
                return Assertion.Failure($"\"{node}\" is not present in response: \n\n{operation.Response.ToHttpLook(operation)}");

            var value = token.Value<string>();
            if (String.IsNullOrWhiteSpace(value))
            {
                return Assertion.Failure($"\"{node}\" is empty in response: \n\n{operation.Response.ToHttpLook(operation)}");
            }

            return Assertion.Ok;

        }

        public static IEnumerable<IAssertion> Http404NotFound()
        {
            // Request
            yield return ApiConventionFor.RequestMethodIs(HttpMethod.Get);

            // Response
            yield return ApiConventionFor.ResponseStatusIs(HttpStatusCode.NotFound);
        }

        public static IEnumerable<IAssertion> GetSingleResource()
        {
            // Request
            yield return ApiConventionFor.RequestMethodIs(HttpMethod.Get);

            // Response
            yield return ApiConventionFor.ResponseStatusIs(HttpStatusCode.OK);
            yield return ApiConventionFor.ResponseContentTypeIs(MediaTypeNames.Application.Json);
        }

        public static IEnumerable<IAssertion> GetSingleResourceThatDoNotExist()
        {
            // Request
            yield return ApiConventionFor.RequestMethodIs(HttpMethod.Get);

            // Response
            yield return ApiConventionFor.ResponseStatusIs(HttpStatusCode.NotFound);
            yield return ApiConventionFor.ResponseContentTypeIs(MediaType.Application.ProblemJson);
        }

        public static IEnumerable<IAssertion> DeleteResource()
        {
            // Request
            yield return ApiConventionFor.RequestMethodIs(HttpMethod.Delete);

            // Response
            yield return ApiConventionFor.ResponseStatusIs(HttpStatusCode.OK);
            yield return ApiConventionFor.ResponseContentTypeIs(MediaTypeNames.Application.Json);
        }
    }
}