using System.IO;
using Synergy.Convention.Testing;
using Synergy.Web.Api.Testing;
using Xunit;

namespace Synergy.Sample.Web.API.Tests.Conventions
{
    public class GenerateApiDescription
    {
        [Fact]
        public void Generate()
        {
            // ARRANGE
            var assembly = typeof(TestServer).Assembly;
            
            // ACT
            var d = ApiDescription.GenerateFor(assembly);

            // ASSERT
            File.WriteAllText(@"../../../Conventions/synergy.web.api.testing.md", d);
        }
    }
}