using Synergy.Sample.Web.API.Tests.Infrastructure;
using Synergy.Web.Api.Testing;

namespace Synergy.Sample.Web.API.Tests.Errors
{
    public class ErrorsClient
    {
        private const string Path = "api/v1/i-do-not-exist";
        private readonly TestServer _testServer;

        public ErrorsClient(TestServer testServer)
        {
            this._testServer = testServer;
        }

        public HttpOperation GetNonExistingResource()
            => this._testServer.Get(ErrorsClient.Path)
                   .Details("GET not existing resource")
                   .ShouldBe(ApiConventionFor.Http404NotFound());
    }
}