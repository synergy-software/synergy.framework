using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
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
        [NotNull]
        public abstract HttpClient HttpClient { get; }

        public bool Repair { get; set; }

        public virtual JsonSerializerSettings SerializationSettings { get; }

        public virtual void FailIfLeftInRepairMode()
        {
            Fail.IfTrue(Repair, Violation.Of("Test server is in repair mode. Do not leave it like that."));
        }

        public HttpOperation Get(string path, object? urlParameters = null, Action<HttpRequestHeaders>? headers = null)
            => this.Get<HttpOperation>(path, urlParameters, headers);
        
        public TOperation Get<TOperation>(string path, object? urlParameters = null, Action<HttpRequestHeaders>? headers = null)
            where TOperation : HttpOperation, new() 
            => this.Send<TOperation>(HttpMethod.Get, path, urlParameters, body: null, headers);
        
        public HttpOperation Post(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            => this.Post<HttpOperation>(path, urlParameters, body, headers);

        public TOperation Post<TOperation>(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Post, path, urlParameters, body, headers);

        public HttpOperation Post(string path, object? urlParameters = null, JToken body = null, Action<HttpRequestHeaders>? headers = null)
            => this.Send<HttpOperation>(HttpMethod.Post, path, urlParameters, body, headers);
        
        public HttpOperation Put(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            => this.Put<HttpOperation>(path, urlParameters, body, headers);

        public TOperation Put<TOperation>(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Put, path, urlParameters, body, headers);
        
        public HttpOperation Patch(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            => this.Patch<HttpOperation>(path, urlParameters, body, headers);

        public TOperation Patch<TOperation>(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Patch, path, urlParameters, body, headers);
        
        public HttpOperation Delete(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            => this.Delete<HttpOperation>(path, urlParameters, body, headers);

        public TOperation Delete<TOperation>(string path, object? urlParameters = null, object? body = null, Action<HttpRequestHeaders>? headers = null)
            where TOperation : HttpOperation, new()
            => this.Send<TOperation>(HttpMethod.Delete, path, urlParameters, body, headers);

        private TOperation Send<TOperation>(
            HttpMethod httpMethod,
            string path,
            object? urlParameters,
            object? body,
            Action<HttpRequestHeaders>? headers = null
        )
            where TOperation : HttpOperation, new()
        {
            var requestedOperation = CreateHttpRequest(httpMethod, path, urlParameters, body, headers);

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

        protected virtual HttpRequestMessage CreateHttpRequest(
            HttpMethod httpMethod,
            string path,
            object? urlParameters,
            object? body = null,
            Action<HttpRequestHeaders>? headers = null
        )
        {
            var request = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = PrepareRequestUri(path, urlParameters),
            };

            if (body != null)
            {
                string json = JsonConvert.SerializeObject(body, this.SerializationSettings);
                request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            }

            if (headers != null)
            {
                headers(request.Headers);
            }

            return request;
        }

        private Uri PrepareRequestUri(string path, object? parameterToGet = null)
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