using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synergy.Sample.Web.API.Services.Infrastructure.Annotations;
using Synergy.Sample.Web.API.Services.Infrastructure.Exceptions;

namespace Synergy.Sample.Web.API.Extensions
{
    // TODO: Add swagger filter that shows the possible error 
    [CreatedImplicitly]
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                await this.HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = context.TraceIdentifier;
            var httpStatus = (int) ExceptionHandlingMiddleware.GetResponseStatus(exception);
            // TODO: Log the error id so it would be esier to find the exception occurence when someone sends the id
            // TODO: How to log correlationId

            // TODO: Depending on exception type decide to return (or not) exception details to the client
            var details = new ProblemDetails
                          {
                              Type = $"https://httpstatuses.com/{httpStatus}",
                              Status = httpStatus,
                              Title = exception.Message,
                          };
            details.Extensions.Add("traceId", traceId);

            context.Response.StatusCode = httpStatus;
            context.Response.ContentType = MediaType.Application.ProblemJson;
            var payload = JsonSerializer.Serialize(details);
            return context.Response.WriteAsync(payload);
        }

        private static HttpStatusCode GetResponseStatus(Exception exception)
        {
            if (exception is ResourceNotFoundException)
                return HttpStatusCode.NotFound;

            return HttpStatusCode.BadRequest;
        }

        // TODO: Rozważ zwracanie struktury ProblemDetails - zgodnie z https://tools.ietf.org/html/rfc7807
        // new ProblemDetails(){ Type = "link do kontrolera z opisem problemu"} Dodatkowo zwracaj conten-type=application/problem+json
        // + możliwość zwrócenia wyjątku, który będzie opisywał dokładnie jaki jest problem - lista problemów znana w systemie i śledzona na poziomie Developera
        // + strategia obsługi ValidationErrors - może przez klasę ValidationProblemDetails:
        // https://httpstatuses.com/400 - link do opisu błądów
        //{
        //    "errors": {
        //        "Name": [
        //        "The Name field is required."
        //            ]
        //    },
        //    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        //    "title": "One or more validation errors occurred.",
        //    "status": 400,
        //    "traceId": "|78d9159-4e0a9444dbc9fd14."
        //}
        // private class ErrorDetails
        // {
        //     [JsonConstructor]
        //     public ErrorDetails(string traceId, string message)
        //     {
        //         TraceId = traceId;
        //         Message = message;
        //     }
        //
        //     [JsonProperty("message")]
        //     public string Message { get; }
        //
        //     [JsonProperty("traceId")]
        //     public string TraceId { get; }
        // }
    }

    public class MediaType
    {
        public class Application
        {
            public const string ProblemJson = "application/problem+json";
        }
    }
}