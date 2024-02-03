using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synergy.Contracts;
using Synergy.Web.Api.Testing.Features;
using Synergy.Web.Api.Testing.Json;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class CompareOperationWithPattern : Assertion, IHttpRequestStorage, IHttpResponseStorage
    {
        private readonly string _patternFilePath;
        private readonly Ignore _ignore;
        private JToken? _savedPattern;

        public CompareOperationWithPattern(string patternFilePath, Ignore? ignore = null)
        {
            _patternFilePath = patternFilePath;
            _ignore = ignore ?? new Ignore();
            Ignore(Json.Ignore.RequestDescription());
            Ignore(Json.Ignore.ResponseContentLength());
            if (File.Exists(patternFilePath))
            {
                var content = File.ReadAllText(patternFilePath);
                _savedPattern = JObject.Parse(content);
            }
        }

        public override Result Assert(HttpOperation operation)
        {
            var current = GeneratePattern(operation);
            if (_savedPattern == null)
            {
                SaveNewPattern(current, operation);
                return Ok;
            }

            var patterns = new JsonComparer(_savedPattern, current, _ignore);

            if (operation.TestServer.Repair && patterns.AreEquivalent == false)
            {
                SaveNewPattern(current, operation);
                return Ok;
            }
            
            if (patterns.AreEquivalent)
            {
                return Ok;
            }

            return Failure($"Operation is different than expected. \nVerify the differences: \n\n{patterns.GetDifferences()}");
        }

        private void SaveNewPattern(JObject current, HttpOperation operation)
        {
            _savedPattern = current;
            File.WriteAllText(_patternFilePath,
                current.ToString(
                    Formatting.Indented,
                    operation.TestServer.Converters()
                )
            );
        }

        private static JObject GeneratePattern(HttpOperation operation)
        {
            var properties = GetPatternProperties(operation);
            return new JObject(properties);
        }

        private static IEnumerable<JProperty> GetPatternProperties(HttpOperation operation)
        {
            yield return new JProperty("expectations", GetExpectations(operation));
            yield return new JProperty("request", new JObject(GetRequestProperties(operation)));
            yield return new JProperty("response", new JObject(GetResponseProperties(operation)));
        }

        private static JArray GetExpectations(HttpOperation operation)
        {
            // TODO: Add assertion results
            return new JArray(operation.Assertions.Select(a => a.ExpectedResult));
        }

        private static IEnumerable<JProperty> GetRequestProperties(HttpOperation operation)
        {
            if (string.IsNullOrWhiteSpace(operation.Description) == false)
            {
                yield return new JProperty("description", operation.Description);
            }

            var request = operation.Request;
            yield return new JProperty("method", request.GetRequestFullMethod());

            var headers = operation.GetAllRequestHeaders();
            if (headers.Count > 0)
            {
                yield return new JProperty("headers", new JObject(headers.Select(GetHeader)));
            }

            var requestJson = request.Content.ReadJson();
            if (requestJson != null)
            {
                yield return new JProperty("body", requestJson);
            }
        }

        private static IEnumerable<JProperty> GetResponseProperties(HttpOperation operation)
        {
            var response = operation.Response;

            yield return new JProperty("status", $"{(int) response.StatusCode} {response.ReasonPhrase}");

            var headers = response.GetAllHeaders();
            if (headers.Count > 0)
            {
                yield return new JProperty("headers", new JObject(headers.Select(GetHeader)));
            }

            var responseJson = response.Content.ReadJson();
            if (responseJson != null)
            {
                yield return new JProperty("body", responseJson);
            }
        }

        private static JProperty GetHeader(KeyValuePair<string, IEnumerable<string>> header)
        {
            return new JProperty(header.Key, string.Join("; ", header.Value));
        }

        public CompareOperationWithPattern Ignore(string ignore, params string[] ignores)
        {
            _ignore.Append(new[] {ignore});
            _ignore.Append(ignores);
            return this;
        }

        public CompareOperationWithPattern Ignore(Ignore ignore)
        {
            _ignore.Append(ignore.Nodes);
            return this;
        }

        HttpRequestMessage IHttpRequestStorage.GetSavedRequest()
        {
            Fail.IfNull(_savedPattern);

            var fullMethod = _savedPattern!.SelectToken("$.request.method").Value<string>();
            var method = fullMethod.Substring(0, fullMethod.IndexOf(" "));
            var url = fullMethod.Substring(fullMethod.IndexOf(" "));
            var request = new HttpRequestMessage(new HttpMethod(method), url);

            var body = _savedPattern!.SelectToken("$.request.body");
            if (body != null)
            {
                var payload = body.ToString();
                request.Content = new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json);
            }

            return request;
        }

        HttpResponseMessage IHttpResponseStorage.GetSavedResponse()
        {
            Fail.IfNull(_savedPattern);

            var fullStatus = _savedPattern!.SelectToken("$.response.status").Value<string>();
            var status = fullStatus.Substring(0, fullStatus.IndexOf(" "));
            var statusCode = Enum.Parse<HttpStatusCode>(status);
            var response = new HttpResponseMessage(statusCode);
            var body = _savedPattern!.SelectToken("$.response.body")?.ToString();
            if (body != null)
            {
                response.Content = new StringContent(body);
            }

            var headers = _savedPattern!.SelectTokens("$.response.headers.*");
            foreach (var header in headers)
            {
                var headerName = header.Path.Replace("response.headers.", "");
                var headerValue = header.Value<string>();

                if (headerName.StartsWith("Content"))
                {
                    if (response.Content.Headers.Contains(headerName))
                    {
                        response.Content.Headers.Remove(headerName);
                    }

                    response.Content.Headers.Add(headerName, headerValue);
                    continue;
                }

                response.Headers.Add(headerName, headerValue);
            }

            return response;
        }
    }
}