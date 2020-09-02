using System;
using Newtonsoft.Json.Linq;

namespace Synergy.Web.Api.Testing.Assertions {
    public class VerifyResponseBody : Assertion
    {
        private readonly string _jsonToken;
        private readonly Func<HttpOperation, JToken?, Result> _validate;

        public VerifyResponseBody(string jsonToken, Func<HttpOperation, JToken?, Result> validate)
        {
            _jsonToken = jsonToken;
            _validate = validate;
        }

        public override Result Assert(HttpOperation operation)
        {
            var token = operation.Response.Content.ReadJson()?.SelectToken(_jsonToken);
            return _validate(operation, token);
        }
    }
}