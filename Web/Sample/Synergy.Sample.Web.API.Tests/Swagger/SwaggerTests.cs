using Synergy.Sample.Web.API.Tests.Infrastructure;
using Synergy.Web.Api.Testing;
using Synergy.Web.Api.Testing.Assertions;
using Synergy.Web.Api.Testing.Json;
using Xunit;

namespace Synergy.Sample.Web.API.Tests.Swagger
{
    public class SwaggerTests
    {
        private const string Path = @"../../../Contracts/";

        [Theory]
        [InlineData("v1")]
        public void validate_api_contract_changes(string version)
        {
            // ARRANGE
            using var testServer = new SampleTestServer();
            var swagger = new SwaggerClient(testServer);
            testServer.Repair = Repair.Mode;

            // ACT
            swagger.GetSwaggerContract(version)
                   .ShouldBe(SwaggerTests.EqualToPatternIn(version));
        }

        private static CompareResponseWithPattern EqualToPatternIn(string version)
        {
            return new CompareResponseWithPattern(
                                                  SwaggerTests.Path + $"swagger-{version}.json",
                                                  new Ignore("info.description"),
                                                  CompareResponseWithPattern.Mode.ContractCheck
                                                 );
        }
    }
}
