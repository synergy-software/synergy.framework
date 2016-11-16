using NUnit.Framework;
using Synergy.Core.Libraries;
using Synergy.Core.Sample;
using Synergy.Core.Windsor;

namespace Synergy.Core.Test.Libraries
{
    [TestFixture]
    public class LibrarianTest
    {
        [Test]
        public void GetLibraries()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var librarian = windsorEngine.GetComponent<ILibrarian>();

            // ACT
            Library[] libraries = librarian.GetLibraries();

            // ASSERT
            Assert.That(libraries,
                Is.EquivalentTo(
                    new Library[]
                    {
                        new SynergyCoreTestLibrary(),
                        new SynergyCoreSampleLibrary(),
                        new SynergyCoreLibrary()
                    }));
            windsorEngine.Stop();
        }

        [Test]
        public void GetLibrariesContainsCoreLibraryEvenIfItWasNotSpecifiedAsDependency()
        {
            // ARRANGE
            var librarian = new Librarian(new SynergyCoreSampleLibrary());

            // ACT
            Library[] libraries = librarian.GetLibraries();

            // ASSERT
            Assert.That(libraries,
                Is.EquivalentTo(
                    new Library[]
                    {
                        new SynergyCoreSampleLibrary(),
                        new SynergyCoreLibrary()
                    }));
        }

        [Test]
        public void GetRootLibrary()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var librarian = windsorEngine.GetComponent<ILibrarian>();

            // ACT
            Library rootLibrary = librarian.GetRootLibrary();

            // ASSERT
            Assert.That(rootLibrary.Equals(new SynergyCoreTestLibrary()), Is.True);
            windsorEngine.Stop();
        }
    }
}