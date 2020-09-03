using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Synergy.Sample.Web.API.Extensions;
using Synergy.Web.Api.Testing;

namespace Synergy.Sample.Web.API.Tests.Infrastructure
{
    public class SampleTestServer : TestServer
    {
        protected override HttpClient Start()
        {
            return new WebApplicationFactory<Startup>()
                  .WithWebHostBuilder(configuration => { configuration.UseEnvironment(Application.Environment.Tests); })
                  .CreateClient();
        }
    }
}