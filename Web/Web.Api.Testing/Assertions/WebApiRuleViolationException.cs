using System;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class WebApiRuleViolationException : Exception
    {
        public WebApiRuleViolationException(Assertion.Result result) : base(result.Message) { }
    }
}