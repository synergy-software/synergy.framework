using ApprovalTests;
using NUnit.Framework;
using Synergy.Convention.Testing;
using Synergy.Markdowns.Test;

namespace Synergy.Contracts.Test.Conventions
{
    [TestFixture]
    public class GenerateApiDescription
    {
        [Test]
        public void Generate()
        {
            // ARRANGE
            var assembly = typeof(Fail).Assembly;

            // ACT
            var publicApi = ApiDescription.GenerateFor(assembly);

            // ASSERT
            var writer = new MarkdownTextWriter(publicApi);
            var approvalNamer = new PublicApi.PublicApiGenerator.AssemblyPathNamer(assembly);
            Approvals.Verify(writer, approvalNamer, Approvals.GetReporter());
        }
    }
}