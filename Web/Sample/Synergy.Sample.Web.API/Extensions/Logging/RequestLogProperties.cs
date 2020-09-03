namespace Synergy.Sample.Web.API.Extensions.Logging
{
    /// <example>
    /// {
    /// "Timestamp":"2020-01-09T12:02:18.7077279+01:00",
    /// "Level":"Information",
    /// "MessageTemplate":"HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms",
    /// "Properties":{
    ///     "RequestHost":"localhost:5001",
    ///     "RequestScheme":"https",
    ///     "RequestMethod":"GET",
    ///     "RequestPath":"/swagger/v1/swagger.json",
    ///     "StatusCode":200,
    ///     "Elapsed":248.2661,
    ///     "SourceContext":"Serilog.AspNetCore.RequestLoggingMiddleware",
    ///     "RequestId":"0HLSKUDHA6HAF:00000003",
    ///     "SpanId":"|5137a515-4fc3655aa92aa470.",
    ///     "TraceId":"5137a515-4fc3655aa92aa470",
    ///     "ParentId":"",
    ///     "MachineName":"LAPTOP",
    ///     "EnvironmentName":"Development",
    ///     "EnvironmentUserName":"SYNERGY\\marcin",
    ///     "ApplicationVersion":"1.0.0.0",
    ///     "ApplicationName":"Synergy Samples"
    /// },
    /// "Renderings":{
    ///     "Elapsed":[
    ///     {
    ///         "Format":"0.0000",
    ///         "Rendering":"248.2661"
    ///     }
    ///     ]
    /// }
    /// }
    /// </example>
    public static class RequestLogProperties
    {
        /// <summary>
        /// Sample: "RequestMethod":"GET"
        /// </summary>
        public const string RequestMethod = "RequestMethod";

        /// <summary>
        /// Sample: "RequestScheme":"https"
        /// </summary>
        public const string RequestScheme = "RequestScheme";

        /// <summary>
        /// Sample: "RequestHost":"localhost:5001"
        /// </summary>
        public const string RequestHost = "RequestHost";

        /// <summary>
        /// Sample: "RequestPath":"/swagger/v1/swagger.json"
        /// </summary>
        public const string RequestPath = "RequestPath";

        /// <summary>
        /// Sample: "StatusCode":200
        /// </summary>
        public const string ResponseStatus = "StatusCode";

        /// <summary>
        /// Sample: "Elapsed":248.2661
        /// </summary>
        public const string RequestDuration = "Elapsed";

        /// <summary>
        /// Sample: "RequestId":"0HLSKUDHA6HAF:00000003"
        /// </summary>
        public const string RequestId = "RequestId";

        /// <summary>
        /// "SpanId":"|5137a515-4fc3655aa92aa470."
        /// </summary>
        public const string SpanId = "SpanId";

        /// <summary>
        /// "TraceId":"5137a515-4fc3655aa92aa470"
        /// </summary>
        public const string TraceId = "TraceId";
    }
}