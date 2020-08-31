using System;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class VerifyResponseHeader : Assertion
    {
        private readonly string _headerName;
        private readonly Func<HttpOperation, string, Result> _validate;

        public VerifyResponseHeader(string headerName, Func<HttpOperation, string, Result> validate)
        {
            _headerName = headerName;
            _validate = validate;
        }

        public override Result Assert(HttpOperation operation)
        {
            operation.Response.Headers.TryGetValues(_headerName, out var values);
            values ??= new string[1];
            foreach (var value in values)
            {
                var result = _validate(operation, value);
                if (result.IsOk == false)
                    return result;
            }

            return Ok;
        }
    }
}