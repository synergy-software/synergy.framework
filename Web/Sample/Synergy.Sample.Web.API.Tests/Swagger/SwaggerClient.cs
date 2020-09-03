using System.Diagnostics.CodeAnalysis;
using Synergy.Web.Api.Testing;

namespace Synergy.Sample.Web.API.Tests.Swagger
{
    public class SwaggerClient
    {
        private readonly TestServer _testServer;

        public SwaggerClient(TestServer testServer)
        {
            this._testServer = testServer;
        }

        public HttpOperation GetSwaggerContract([NotNull] string version) 
            => this._testServer.Get($"/swagger/{version}/swagger.json");
    }
}