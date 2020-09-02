using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synergy.Contracts;

namespace Synergy.Web.Api.Testing
{
    public abstract class TestServer
    {
        public HttpClient HttpClient { get; }
        public bool Repair { get; set; }

        protected TestServer()
        {
            HttpClient = Start();
        }

        protected abstract HttpClient Start();

        public virtual void FailIfLeftInRepairMode()
        {
            Fail.IfTrue(Repair, Violation.Of("Test server is in repair mode. Do not leave it like that."));
        }

        public HttpOperation Get(string path, [CanBeNull] object? urlParameters = null)
            => Send<HttpOperation>(HttpMethod.Get, path, urlParameters);

        public HttpOperation Post(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            => Send<HttpOperation>(HttpMethod.Post, path, urlParameters, body);

        public HttpOperation Post(string path, [CanBeNull] object? urlParameters = null, JToken body = null)
            => Send<HttpOperation>(HttpMethod.Post, path, urlParameters, body);

        public TOperation Post<TOperation>(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            where TOperation : HttpOperation, new()
            => Send<TOperation>(HttpMethod.Post, path, urlParameters, body);

        public HttpOperation Put(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            => Send<HttpOperation>(HttpMethod.Put, path, urlParameters, body);

        public HttpOperation Patch(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            => Send<HttpOperation>(HttpMethod.Patch, path, urlParameters, body);

        public HttpOperation Delete(string path, [CanBeNull] object? urlParameters = null)
            => Send<HttpOperation>(HttpMethod.Delete, path, urlParameters);

        private TOperation Send<TOperation>(HttpMethod httpMethod, string path, object? urlParameters, object? body = null) 
            where TOperation : HttpOperation, new()
        {
            var requestedOperation = CreateHttpRequest(httpMethod, path, urlParameters, body);

            var request = CreateHttpRequest(httpMethod, path, urlParameters, body);
            var timer = Stopwatch.StartNew();
            Task<HttpResponseMessage> task = this.HttpClient.SendAsync(request);
            task.Wait();
            var response = task.Result;
            timer.Stop();

            var operation = new TOperation();
            operation.Init(this, requestedOperation, response, timer);
            return operation;
        }

        private HttpRequestMessage CreateHttpRequest(HttpMethod httpMethod, string path, object? urlParameters, object? body = null)
        {
            var request = new HttpRequestMessage
                          {
                              Method = httpMethod,
                              RequestUri = PrepareRequestUri(path, urlParameters)
                          };

            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaTypeNames.Application.Json);
            }

            return request;
        }

        private Uri PrepareRequestUri(string path, [CanBeNull] object? parameterToGet = null)
        {
            Fail.IfWhitespace(path, nameof(path));

            var uriBuilder = new UriBuilder
                             {
                                 Path = path.Replace("http://localhost/", ""),
                                 Host = HttpClient.BaseAddress.Host,
                                 Port = HttpClient.BaseAddress.Port
                             };

            if (parameterToGet != null)
                uriBuilder.Query = QueryBuilder.Build(parameterToGet);

            return uriBuilder.Uri;
        }
    }
}