using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Synergy.Sample.Web.API.Extensions;
using Synergy.Web.Api.Testing;

namespace Synergy.Sample.Web.API.Tests.Infrastructure
{
    public class SampleTestServer : TestServer
    {
        /// <inheritdoc />
        public override JsonSerializerSettings SerializationSettings => new()
        {
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
        };

        private WebApplicationFactory<Startup>? webApplicationFactory;

        /// <inheritdoc />
        public override HttpClient HttpClient { get; }
        
        public SampleTestServer()
        {
            this.HttpClient = this.Start();
        }

        private HttpClient Start()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient httpClient = this.webApplicationFactory
                                        .WithWebHostBuilder(configuration => { configuration.UseEnvironment(Application.Environment.Tests); })
                                        .CreateClient();
            httpClient.DefaultRequestHeaders.Add("test", "header");
            
            return httpClient;
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            this.webApplicationFactory?.Dispose();
            base.Dispose();
        }
    }
}