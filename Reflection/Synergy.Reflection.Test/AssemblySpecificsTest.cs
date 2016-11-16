using System.Reflection;
using NUnit.Framework;

namespace Synergy.Reflection.Test
{
    [TestFixture]
    public class AssemblySpecificsTest
    {
        [Test]
        public void Off()
        {
            // ARRANGE
            Assembly assembly = this.GetType().Assembly;

            // ACT
            var assemblyeSpecifics = AssemblySpecifics.Of(assembly);

            // ASSERT
            Assert.That(assemblyeSpecifics, Is.Not.Null);
        }

        [Test]
        public void GetVersion()
        {
            // ARRANGE
            var assemblySpecifics = AssemblySpecifics.Of<AssemblySpecificsTest>();

            // ACT
            var version = assemblySpecifics.GetVersion();

            // ASSERT
            Assert.That(version, Is.EqualTo(this.GetType().Assembly.GetName().Version.ToString()));
        }

        [Test]
        public void GetFileVersion()
        {
            // ARRANGE
            var assemblySpecifics = AssemblySpecifics.Of<AssemblySpecificsTest>();

            // ACT
            var version = assemblySpecifics.GetFileVersion();

            // ASSERT
            Assert.That(version, Is.EqualTo(this.GetType().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version));
        }

        [Test]
        public void GetProduct()
        {
            // ARRANGE
            var assemblySpecifics = AssemblySpecifics.Of<AssemblySpecificsTest>();

            // ACT
            var product = assemblySpecifics.GetProduct();

            // ASSERT
            Assert.That(product, Is.EqualTo("Synergy Framework"));
        }
    }
}
