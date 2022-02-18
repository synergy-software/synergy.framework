using System.Collections.Generic;
using System.Globalization;
using ApprovalTests.Namers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Synergy.Contracts;

namespace Synergy.Catalogue.Approval
{
    public static class JsonSettings
    {
        public static JsonSerializerSettings Default
            => new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Culture = CultureInfo.InvariantCulture,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

        public static JsonSerializerSettings WithDateTimeFormatted(this JsonSerializerSettings settings, string format = "yyyy-MM-dd HH:mm:ss")
        {
            settings.DateFormatString = format;
            return settings;
        }

        public static JsonSerializerSettings WithIndents(this JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            return settings;
        }
    }

    public static class JsonExtensions
    {
        public static Json<T> ToJson<T>(this T value, JsonSerializerSettings? settings = null)
        {
            return new Json<T>(JsonConvert.SerializeObject(value, settings ?? JsonSettings.Default));
        }
    }

    public readonly struct Json<T>
    {
        private string Content { get; }

        public Json(string content)
        {
            Fail.IfArgumentWhiteSpace(content, nameof(content));

            this.Content = content;
        }

        public override string ToString()
            => this.Content;

        public string ToString(Formatting formatting)
        {
            JToken parsedJson = JToken.Parse(this.Content);
            return parsedJson.ToString(formatting);
        }

        public Json<T> Ignore(params string[] ignoredPaths)
        {
            Fail.IfArgumentNull(ignoredPaths, nameof(ignoredPaths));

            var json = this;
            foreach (var ignoredPath in ignoredPaths)
                json = json.Replace(ignoredPath, "__IGNORED_VALUE__");

            return json;
        }

        public Json<T> Replace(string jsonPath, string value)
        {
            Fail.IfArgumentWhiteSpace(jsonPath, nameof(jsonPath));
            Fail.IfArgumentWhiteSpace(value, nameof(value));

            var json = JToken.Parse(this.Content);
            foreach (var token in json.SelectTokens(jsonPath))
                switch (token)
                {
                    case JValue jValue:
                        jValue.Value = value;
                        break;
                    case JArray jArray:
                        jArray.Clear();
                        jArray.Add(value);
                        break;
                }

            return new Json<T>(json.ToString(Formatting.None));
        }

        public T Deserialize(JsonSerializerSettings? settings = null)
        {
            return JsonConvert.DeserializeObject<T>(this.Content, settings ?? JsonSettings.Default);
        }
    }

    public static class JsonApprovalsExtensions
    {
        public static void Approve<T>(this Json<T> json)
        {
            ApprovalTests.Approvals.VerifyJson(json.ToString());
        }

        public static void ApproveForScenario<T>(this Json<T> json, string scenario)
        {
            Fail.IfArgumentWhiteSpace(scenario, nameof(scenario));

            using (ApprovalResults.ForScenario(scenario))
            {
                json.Approve();
            }
        }
    }
}