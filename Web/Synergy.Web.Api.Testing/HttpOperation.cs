using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Web.Api.Testing.Assertions;

namespace Synergy.Web.Api.Testing
{
    public class HttpOperation
    {
        [CanBeNull]
        public string? Description { get; private set; }
        public TimeSpan Duration { get; private set; }
        public TestServer TestServer { get; private set; }
        public HttpRequestMessage Request { get; private set; }
        public HttpResponseMessage Response { get; private set; }
        public readonly List<IAssertion> Assertions = new List<IAssertion>();

        internal void Init(TestServer testServer, HttpRequestMessage request, HttpResponseMessage response, Stopwatch timer)
        {
            Duration = timer.Elapsed;
            TestServer = testServer.OrFail();
            Request = request.OrFail();
            Response = response.OrFail();
        }

        internal void Assert(IEnumerable<IAssertion> assertions)
        {
            foreach (var assertion in assertions)
            {
                Assertions.Add(assertion);
                var result = assertion.Assert(this);
                if (result.IsOk == false && TestServer.Repair == false)
                {
                    throw new WebApiRuleViolationException(result);
                }
            }
        }

        internal void SetDescription([NotNull] string details)
        {
            Description = details.OrFailIfWhiteSpace(nameof(details));
        }
    }
}