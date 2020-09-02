using System.Net.Http;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class VerifyRequestMethod : Assertion
    {
        private readonly HttpMethod _expectedMethod;

        public VerifyRequestMethod(HttpMethod expectedMethod)
        {
            _expectedMethod = expectedMethod;
            ExpectedResult = $"HTTP request method is {_expectedMethod}";
        }

        public override Result Assert(HttpOperation operation)
        {
            var actualMethod = operation.Request.Method;
            if (actualMethod == _expectedMethod)
            {
                return Ok;
            }

            return Failure(
                $"Expected HTTP method is {_expectedMethod} but was {actualMethod} " +
                $"in request: \n\n{operation.Request.ToHttpLook()}");
        }
    }
}