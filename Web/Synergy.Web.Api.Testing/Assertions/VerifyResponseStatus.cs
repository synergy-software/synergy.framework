using System.Net;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class VerifyResponseStatus : Assertion
    {
        private readonly HttpStatusCode _expectedStatus;

        public VerifyResponseStatus(HttpStatusCode expectedStatus)
        {
            _expectedStatus = expectedStatus;
            ExpectedResult = $"Returned HTTP status code is {(int) _expectedStatus} ({_expectedStatus})";
        }

        public override Result Assert(HttpOperation operation)
        {
            var actualStatus = operation.Response.StatusCode;
            if (_expectedStatus == actualStatus)
            {
                return Ok;
            }

            return Failure(
                $"Expected HTTP status is {_expectedStatus} but was {actualStatus} " +
                $"in response: \n\n{operation.Response.ToHttpLook()}");
        }
    }
}