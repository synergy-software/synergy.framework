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
    public abstract class TestServer : IDisposable
    {
        public HttpClient HttpClient { get; }
        public bool Repair { get; set; }

        [CanBeNull] 
        protected virtual JsonSerializerSettings serializationSettings { get; }

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
            => this.Get<HttpOperation>(path, urlParameters);
        
        public TOperation Get<TOperation>(string path, [CanBeNull] object? urlParameters = null)
            where TOperation : HttpOperation, new() 
            => this.Send<TOperation>(HttpMethod.Get, path, urlParameters);
        
        public HttpOperation Post(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            => this.Post<HttpOperation>(path, urlParameters, body);

        public TOperation Post<TOperation>(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Post, path, urlParameters, body);

        public HttpOperation Post(string path, [CanBeNull] object? urlParameters = null, JToken body = null)
            => this.Send<HttpOperation>(HttpMethod.Post, path, urlParameters, body);
        
        public HttpOperation Put(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            => this.Put<HttpOperation>(path, urlParameters, body);

        public TOperation Put<TOperation>(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Put, path, urlParameters, body);
        
        public HttpOperation Patch(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            => this.Patch<HttpOperation>(path, urlParameters, body);

        public TOperation Patch<TOperation>(string path, [CanBeNull] object? urlParameters = null, object? body = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Patch, path, urlParameters, body);
        
        public HttpOperation Delete(string path, [CanBeNull] object? urlParameters = null)
            => this.Delete<HttpOperation>(path, urlParameters);

        public TOperation Delete<TOperation>(string path, [CanBeNull] object? urlParameters = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Delete, path, urlParameters);
        
        private TOperation Send<TOperation>(HttpMethod httpMethod, string path, object? urlParameters, object? body = null) 
            where TOperation : HttpOperation, new()
        {
            var requestedOperation = CreateHttpRequest(httpMethod, path, urlParameters, body);

            using var request = CreateHttpRequest(httpMethod, path, urlParameters, body);
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
                string json = JsonConvert.SerializeObject(body, this.serializationSettings);
                request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
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

        /// <inheritdoc />
        public virtual void Dispose()
        {
            this.HttpClient?.Dispose();
        }
    }
}