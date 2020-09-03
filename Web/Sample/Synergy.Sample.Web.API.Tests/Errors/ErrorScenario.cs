using System;
using System.Net;
using JetBrains.Annotations;
using Synergy.Sample.Web.API.Tests.Infrastructure;
using Synergy.Web.Api.Testing;
using Synergy.Web.Api.Testing.Assertions;
using Synergy.Web.Api.Testing.Features;
using Xunit;

namespace Synergy.Sample.Web.API.Tests.Errors
{
    public class ErrorScenario:IDisposable
    {
        private SampleTestServer testServer;
        private ErrorsClient errors;
        private const string Path = @"../../../Errors";
        private readonly Feature feature = new Feature("Check API errors");

        public ErrorScenario()
        {
            this.testServer = new SampleTestServer();
            this.errors = new ErrorsClient(this.testServer);
            this.testServer.Repair = Repair.Mode;
        }

        public void Dispose()
        {
            this.testServer.Dispose();
        }
        
        [Fact]
        public void check_errors()
        {
            // SCENARIO
            this.Get404NotFound();

            new Markdown(this.feature).GenerateReportTo(ErrorScenario.Path + "/Errors.md");
        }

        private void Get404NotFound()
        {
            var scenario = this.feature.Scenario("Get not existing resource from API");

            this.errors.GetNonExistingResource()
                .InStep(scenario.Step("Try to retrieve not existing resource"))
                  
                .ShouldBe(
                    this.EqualToPattern("/Patterns/Get404.json")
                        .Expected("Manual: Error is returned")
                );
        }

        private CompareOperationWithPattern EqualToPattern([PathReference] string file)
            => new CompareOperationWithPattern(ErrorScenario.Path + file);

        private static VerifyResponseStatus InStatus(HttpStatusCode expected) 
            => new VerifyResponseStatus(expected);
    }
}