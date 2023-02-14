using System;
using System.Linq;
using System.Net.Http;
using Synergy.Contracts;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class VerifyRequestMethod : Assertion
    {
        private readonly HttpMethod[] _expectedMethod;

        public VerifyRequestMethod(params HttpMethod[] expectedMethod)
        {
            _expectedMethod = expectedMethod.OrFail(nameof(expectedMethod));
            ExpectedResult = $"HTTP request method is {_expectedMethod}";
        }

        public override Result Assert(HttpOperation operation)
        {
            var actualMethod = operation.Request.Method;
            if (_expectedMethod.Contains(actualMethod))
            {
                return Ok;
            }

            var methods = String.Join(", ", _expectedMethod.Select(m=>m.ToString()));

            return Failure(
                $"Expected HTTP method is ({methods}) but was {actualMethod} " +
                $"in request: \n\n{operation.Request.ToHttpLook(operation)}");
        }
    }
}