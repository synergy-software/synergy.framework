using NUnit.Framework;
using Synergy.Convention.Testing;
using VerifyXunit;

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
            Verifier.Verify(publicApi, "md")
                    .UseMethodName(assembly.GetName().Name);
        }
    }
}