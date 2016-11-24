using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Synergy.Core.Sample.Users;
using Synergy.Core.Test.Users;
using Synergy.Core.Windsor;

namespace Synergy.Core.Test.Windsor
{
    [TestFixture]
    public class WindsorEngineTest
    {
        [Test]
        public void can_start_windsor_engine()
        {
            // ARRANGE
            var rootLibrary = new SynergyCoreTestLibrary();
            using (IWindsorEngine windsorEngine = new WindsorEngine())
            {
                // ACT
                windsorEngine.Start(rootLibrary);

                // ASSERT
                var component = windsorEngine.GetComponent<IComponentMock>();
                Assert.IsAssignableFrom<ComponentMock>(component);
            }
        }

        [Test]
        public void component_list_can_be_retrieved_from_windsor_engine()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var componentLocator = windsorEngine.GetComponent<IComponentLocator>();

            // ACT
            IUserRepository[] components = componentLocator.GetComponents<IUserRepository>();

            // ASSERT
            Assert.NotNull(components);
            Assert.That(components, Is.Not.Empty);
            Assert.That(components.Length, Is.EqualTo(2));
            windsorEngine.Stop();
        }

        [Test]
        public void windsor_engine_is_available_as_component()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();

            // ACT
            var engineTakenFromWindsor = windsorEngine.GetComponent<IWindsorEngine>();

            // ASSERT
            Assert.AreEqual(windsorEngine, engineTakenFromWindsor);
            windsorEngine.Stop();
        }

        [Test]
        public void can_insert_dependent_collection_of_components()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();

            // ACT
            var component = windsorEngine.GetComponent<IComponentMock>();

            // ASSERT
            Assert.That(component, Is.Not.Null);
            IEnumerable<IDependentComponentMock> dependencies = component.GetDependencies();
            Assert.NotNull(dependencies);
            Assert.AreEqual(1, dependencies.Count());
            windsorEngine.Stop();
        }

        [Test]
        public void components_are_singletons_by_default()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();

            // ACT
            var component1 = windsorEngine.GetComponent<IComponentMock>();
            var component2 = windsorEngine.GetComponent<IComponentMock>();

            // ASSERT
            Assert.That(component1, Is.Not.Null);
            Assert.That(component1, Is.EqualTo(component2));
            windsorEngine.Stop();
        }

        [Test]
        public void component_marked_as_transient_is_really_so()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();

            // ACT
            var transient1 = windsorEngine.GetComponent<ITransientComponentMock>();
            var transient2 = windsorEngine.GetComponent<ITransientComponentMock>();

            // ASSERT
            Assert.That(transient1, Is.Not.EqualTo(transient2));
            windsorEngine.Stop();
        }

        [Test]
        public void component_can_be_registered_using_factory_method()
        {
            // ARRANGE
            // see: https://github.com/castleproject/Windsor/blob/master/docs/registering-components-one-by-one.md
            IWindsorEngine windsorEngine = ApplicationServer.Start();

            // ACT
            var transient1 = windsorEngine.GetComponent<IComponentCreatedViaFactory>();
            var transient2 = windsorEngine.GetComponent<IComponentCreatedViaFactory>();
            var c1 = windsorEngine.GetComponent<IContainerForComponentCreatedViaFactory>();
            var c2 = windsorEngine.GetComponent<IContainerForComponentCreatedViaFactory>();

            // ASSERT
            Assert.That(transient1, Is.Not.EqualTo(transient2));
            Assert.That(c1, Is.EqualTo(c2));
            Assert.That(c1.GetComponentCreatedViaFactory(), Is.EqualTo(c2.GetComponentCreatedViaFactory()));

            windsorEngine.Stop();
        }

        [Test]
        public void component_can_be_overriden()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();

            // ACT
            var userRpository = windsorEngine.GetComponent<IUserRepository>();

            // ASSERT
            Assert.NotNull(userRpository);
            Assert.IsAssignableFrom<UserRepositoryMock>(userRpository);
        }
    }
}