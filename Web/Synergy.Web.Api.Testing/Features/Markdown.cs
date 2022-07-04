using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Synergy.Web.Api.Testing.Features
{
    public class Markdown
    {
        private readonly Feature _feature;

        public Markdown(Feature feature)
        {
            _feature = feature;
        }

        public string GenerateReportTo(string? filePath = null)
        {
            // TODO:Marcin Celej: Do not regenerate report if every operation was 'unchanged' (check somehow HttpOperation.Assertions) - to achieve this add Status to HttpOperation
            
            StringBuilder report = new StringBuilder();
            report.AppendLine($"# {_feature.Title}");
            report.AppendLine();

            InsertTableOfContents(report);

            foreach (var scenario in _feature.Scenarios)
            {
                report.AppendLine();
                report.AppendLine($"## {GetScenarioTitle(scenario)}");
                report.AppendLine();
                InsertScenarioStatusTable(report, scenario);
                report.AppendLine();
                
                foreach (var step in scenario.Steps)
                {
                    report.AppendLine($"### {scenario.No}.{step.No}. {step.Title} ({step.Operations.Count} request{GetPluralSuffix(step.Operations)})");
                    report.AppendLine();

                    foreach (var operation in step.Operations)
                    {
                        report.AppendLine($"### {scenario.No}.{step.No}.{step.No}. {GetOperationRequestTitle(operation)}");
                        report.AppendLine();
                        //report.AppendLine($"<details><summary>Details</summary>");
                        //report.AppendLine();
                        InsertRequest(report, operation);
                        report.AppendLine();
                        InsertResponse(report, operation);
                        report.AppendLine();
                        InsertOperationResponseStatusTable(report, operation);
                        report.AppendLine();
                        //report.AppendLine("</details>");
                    }

                    report.AppendLine();
                }
            }

            var reportText = report.ToString();
            if (filePath != null)
                File.WriteAllText(filePath, reportText);

            return reportText;
        }

        private string GetPluralSuffix<T>(IEnumerable<T> elements)
            => elements.Count() == 1 ? "" : "s";

        private void InsertTableOfContents(StringBuilder report)
        {
            foreach (var scenario in _feature.Scenarios)
            {
                report.AppendLine($"1. [{scenario.Title}](#{GetUrlTo(GetScenarioTitle(scenario))})");
            }
        }

        private string GetScenarioTitle(Scenario scenario) 
            => $"{scenario.No}. {scenario.Title} ({scenario.Steps.Count} step{GetPluralSuffix(scenario.Steps)})";

        private string GetUrlTo(string header)
        {
            var url = header.ToLower().Replace(" ", "-");
            return Regex.Replace(url, "[^a-zA-Z0-9_-]+", "", RegexOptions.Compiled);
        }

        private static void InsertScenarioStatusTable(StringBuilder report, Scenario scenario)
        {
            report.AppendLine("| # | Step Actions | Status |");
            report.AppendLine("|---|--------------|--------|");
            foreach (var step in scenario.Steps)
            {
                report.AppendLine($"| {step.No} | {step.Title} | OK |");
            }
        }

        private static void InsertOperationResponseStatusTable(StringBuilder report, HttpOperation operation)
        {
            report.AppendLine($"| Expected Results  | Status |");
            report.AppendLine($"|-------------------|--------|");
            foreach (var assertion in operation.Assertions.Cast<IExpectation>())
            {
                report.AppendLine($"| {assertion.ExpectedResult} | OK |");
            }
        }

        private static void InsertRequest(StringBuilder report, HttpOperation operation)
        {
            var request = GetHttpRequestFrom(operation);

            report.AppendLine("- Request");
            report.AppendLine("```");
            report.AppendLine(request.ToHttpLook(operation.TestServer.HttpClient.DefaultRequestHeaders));
            report.AppendLine("```");
        }

        private static HttpRequestMessage GetHttpRequestFrom(HttpOperation operation)
        {
            var pattern = operation.Assertions.FirstOrDefault(a => a is IHttpRequestStorage);
            if (pattern != null)
            {
                //
                // Info: If there is saved request it will be used instead of the new one
                //       - it prevents too many changes in markdown as saved pattern is rarely changed
                //
                return ((IHttpRequestStorage) pattern).GetSavedRequest();
            }

            return operation.Request;
        }

        private static string GetOperationRequestTitle(HttpOperation operation)
        {
            var description = operation.Description;
            if (String.IsNullOrWhiteSpace(description))
                return "Request";

            return $"Request to [{description}]";
        }

        private static void InsertResponse(StringBuilder report, HttpOperation operation)
        {
            var response = GetHttpResponseFrom(operation);

            report.AppendLine("- Response");
            report.AppendLine("```");
            report.AppendLine(response.ToHttpLook());
            report.AppendLine("```");
        }

        private static HttpResponseMessage GetHttpResponseFrom(HttpOperation operation)
        {
            var pattern = operation.Assertions.FirstOrDefault(a => a is IHttpResponseStorage);
            if (pattern != null)
            {
                //
                // Info: If there is saved response it will be used instead of the new one
                //       - it prevents too many changes in markdown as saved pattern is rarely changed
                //
                return ((IHttpResponseStorage) pattern).GetSavedResponse();
            }

            return operation.Response;
        }
    }
}