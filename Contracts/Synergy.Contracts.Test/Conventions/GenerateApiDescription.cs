using System.IO;
using NUnit.Framework;
using Synergy.Convention.Testing;

namespace Synergy.Contracts.Test.Conventions
{
    [TestFixture]
    public class GenerateApiDescription
    {
        [Test]
        [Ignore("Doesn't work on build server for unknown reasons")]
        public void Generate()
        {
            // ARRANGE
            var assembly = typeof(Fail).Assembly;
            
            // ACT
            var d = ApiDescription.GenerateFor(assembly);

            // ASSERT
            File.WriteAllText(@"../../../Conventions/synergy.contracts.md", d);
        }
    }
}