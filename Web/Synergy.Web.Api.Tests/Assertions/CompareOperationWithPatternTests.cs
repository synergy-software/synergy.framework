using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synergy.Web.Api.Testing;
using Synergy.Web.Api.Testing.Assertions;
using Synergy.Web.Api.Testing.Features;

namespace Synergy.Web.Api.Tests.Assertions;

public class CompareOperationWithPatternTests
{
    [Theory]
    [InlineData("Allow", new[] { "GET", "POST" }, "GET, POST")]
    [InlineData("Allow", new[] { "GET, POST" }, "GET, POST")]
    [InlineData("Allow", new[] { "GET" }, "GET")]
    [InlineData("allow", new[] { "GET", "POST" }, "GET, POST")]
    public void Allow_round_trips_through_snapshot_and_markdown(
        string headerName, string[] methods, string expectedValue)
    {
        var patternPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        var reportPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        try
        {
            using var server = new StubTestServer(headerName, methods);
            var operation = server.Delete("/forms");
            using var request = operation.Request;
            using var response = operation.Response;
            operation.ShouldBe(new CompareOperationWithPattern(patternPath));

            var snapshot = JObject.Parse(File.ReadAllText(patternPath));
            Assert.Equal(expectedValue, snapshot["response"]!["content"]!["headers"]!["Allow"]!.Value<string>());
            Assert.Equal(
                "application/problem+json; charset=utf-8",
                snapshot["response"]!["content"]!["headers"]!["Content-Type"]!.Value<string>());

            var savedPattern = new CompareOperationWithPattern(patternPath);
            operation.Assertions.Clear();
            operation.ShouldBe(savedPattern);

            using var savedResponse = ((IHttpResponseStorage)savedPattern).GetSavedResponse();
            Assert.Equal(operation.Response.Content.Headers.Allow, savedResponse.Content.Headers.Allow);
            Assert.Equal(
                operation.Response.Content.Headers.ContentType!.ToString(),
                savedResponse.Content.Headers.ContentType!.ToString());

            var feature = new Feature("Unsupported methods");
            feature.Scenario("Forms").Step("Delete forms").Operations.Add(operation);
            var report = new Markdown(feature).GenerateReportTo(reportPath);

            Assert.Contains($"{headerName}: {expectedValue}", report, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("405 MethodNotAllowed", report);
            Assert.Equal(report, File.ReadAllText(reportPath));
        }
        finally
        {
            File.Delete(patternPath);
            File.Delete(reportPath);
        }
    }

    private sealed class StubTestServer : TestServer
    {
        public override HttpClient HttpClient { get; }
        public override JsonSerializerSettings SerializationSettings { get; } = new();

        public StubTestServer(string headerName, string[] methods)
        {
            HttpClient = new HttpClient(new ResponseHandler(headerName, methods))
            {
                BaseAddress = new Uri("http://localhost")
            };
        }
    }

    private sealed class ResponseHandler : HttpMessageHandler
    {
        private readonly string _headerName;
        private readonly string[] _methods;

        public ResponseHandler(string headerName, string[] methods)
        {
            _headerName = headerName;
            _methods = methods;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
            {
                Content = new StringContent(
                    "{\"title\":\"Method Not Allowed\",\"status\":405}",
                    Encoding.UTF8,
                    "application/problem+json")
            };
            response.Content.Headers.Add(_headerName, _methods);
            return Task.FromResult(response);
        }
    }
}
