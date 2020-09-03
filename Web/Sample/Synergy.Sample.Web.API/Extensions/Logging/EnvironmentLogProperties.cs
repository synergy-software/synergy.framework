using Serilog.Enrichers;

namespace Synergy.Sample.Web.API.Extensions.Logging
{
    // TODO: Logging: consider adding explicit EventId to each event
    // TODO: Add dedicated components for logging

    /// <example>
    /// {
    /// "Timestamp":"2020-01-09T13:25:18.5688820+01:00",
    /// "Level":"Information",
    /// "MessageTemplate":"Starting web host on machine {MachineName}",
    /// "Properties":{
    ///     "MachineName":"LAPTOP",
    ///     "EnvironmentUserName":"SYNERGY\\marcin",
    ///     "ApplicationVersion":"1.0.0.0",
    ///     "ApplicationName":"Synergy Samples"
    /// }
    /// }
    /// </example>
    public static class EnvironmentLogProperties
    {
        /// <summary>
        /// Sample: "MachineName":"LAPTOP"
        /// </summary>
        public const string MachineName = MachineNameEnricher.MachineNamePropertyName;

        /// <summary>
        /// Sample: "EnvironmentUserName":"SYNERGY\\marcin"
        /// </summary>
        public const string EnvironmentUserName = EnvironmentUserNameEnricher.EnvironmentUserNamePropertyName;

        /// <summary>
        /// Sample: "EnvironmentName":"Development"
        /// </summary>
        public const string EnvironmentName = "EnvironmentName";

        /// <summary>
        /// Sample: "ApplicationName":"Synergy Samples"
        /// </summary>
        public const string ApplicationName = "ApplicationName";

        /// <summary>
        /// Sample: "ApplicationVersion":"1.0.0.0"
        /// </summary>
        public const string ApplicationVersion = "ApplicationVersion";
    }
}