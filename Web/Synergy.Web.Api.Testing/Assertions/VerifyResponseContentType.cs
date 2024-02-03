using Synergy.Contracts;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class VerifyResponseContentType : Assertion
    {
        private readonly string _expectedContentType;

        public VerifyResponseContentType(string expectedContentType)
        {
            _expectedContentType = expectedContentType.NotNull();
            ExpectedResult = $"Returned HTTP Content-Type is \"{_expectedContentType}\"";
        }

        public override Result Assert(HttpOperation operation)
        {
            var actualContentType = operation.Response.Content.Headers.ContentType.MediaType;
            if (_expectedContentType == actualContentType)
                return Ok;

            return Failure($"Expected HTTP Content-Type is \"{_expectedContentType}\" but was \"{actualContentType}\" " +
                           $"in response: \n\n{operation.Response.ToHttpLook(operation)}");
        }
    }
}