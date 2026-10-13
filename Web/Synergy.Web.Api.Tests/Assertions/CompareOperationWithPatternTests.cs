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
    [InlineData("Expires", new[] { "0" }, "0")]
    [InlineData("Expires", new[] { "-1" }, "-1")]
    [InlineData("Expires", new[] { "not-a-date" }, "not-a-date")]
    [InlineData("Expires", new[] { "Wed, 21 Oct 2015 07:28:00 GMT" }, "Wed, 21 Oct 2015 07:28:00 GMT")]
    [InlineData("Expires", new[] { "Wednesday, 21-Oct-15 07:28:00 GMT" }, "Wed, 21 Oct 2015 07:28:00 GMT")]
    [InlineData("Date", new[] { "not-a-date" }, "not-a-date", false)]
    [InlineData("Date", new[] { "Wed, 21 Oct 2015 07:28:00 GMT" }, "Wed, 21 Oct 2015 07:28:00 GMT", false)]
    public void Response_header_round_trips_through_snapshot_and_markdown(
        string headerName, string[] values, string expectedValue, bool contentHeader = true)
    {
        var patternPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        var reportPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        try
        {
            using var server = new StubTestServer(headerName, values, contentHeader);
            var operation = server.Delete("/forms");
            using var request = operation.Request;
            using var response = operation.Response;
            operation.ShouldBe(new CompareOperationWithPattern(patternPath));

            var snapshot = JObject.Parse(File.ReadAllText(patternPath));
            var headers = (JObject)(contentHeader
                ? snapshot["response"]!["content"]!["headers"]!
                : snapshot["response"]!["headers"]!);
            Assert.Equal(
                expectedValue,
                headers.GetValue(headerName, StringComparison.OrdinalIgnoreCase)!.Value<string>());
            Assert.Equal(
                "application/problem+json; charset=utf-8",
                snapshot["response"]!["content"]!["headers"]!["Content-Type"]!.Value<string>());

            var savedPattern = new CompareOperationWithPattern(patternPath);
            operation.Assertions.Clear();
            operation.ShouldBe(savedPattern);

            using var savedResponse = ((IHttpResponseStorage)savedPattern).GetSavedResponse();
            var savedHeaders = contentHeader ? savedResponse.Content.Headers.NonValidated : savedResponse.Headers.NonValidated;
            Assert.Equal(new[] { expectedValue }, savedHeaders[headerName].ToArray());
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

    [Fact]
    public void Restoring_header_in_wrong_collection_reports_failure()
    {
        var patternPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        try
        {
            File.WriteAllText(patternPath, @"{
                  ""response"": {
                    ""status"": ""200 OK"",
                    ""content"": {
                      ""body"": {},
                      ""headers"": { ""Host"": ""localhost"" }
                    }
                  }
                }");
            var pattern = new CompareOperationWithPattern(patternPath);

            var exception = Assert.Throws<InvalidOperationException>(
                () => ((IHttpResponseStorage)pattern).GetSavedResponse());

            Assert.Contains("Host", exception.Message);
        }
        finally
        {
            File.Delete(patternPath);
        }
    }

    private sealed class StubTestServer : TestServer
    {
        public override HttpClient HttpClient { get; }
        public override JsonSerializerSettings SerializationSettings { get; } = new();

        public StubTestServer(string headerName, string[] values, bool contentHeader)
        {
            HttpClient = new HttpClient(new ResponseHandler(headerName, values, contentHeader))
            {
                BaseAddress = new Uri("http://localhost")
            };
        }
    }

    private sealed class ResponseHandler : HttpMessageHandler
    {
        private readonly string _headerName;
        private readonly string[] _values;
        private readonly bool _contentHeader;

        public ResponseHandler(string headerName, string[] values, bool contentHeader)
        {
            _headerName = headerName;
            _values = values;
            _contentHeader = contentHeader;
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
            System.Net.Http.Headers.HttpHeaders headers = _contentHeader ? response.Content.Headers : response.Headers;
            if (!headers.TryAddWithoutValidation(_headerName, _values))
            {
                throw new InvalidOperationException($"Cannot add test header '{_headerName}'.");
            }
            return Task.FromResult(response);
        }
    }
}
