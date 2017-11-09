using NUnit.Framework;
using Synergy.Core.Windsor;

namespace Synergy.Core.Test.Windsor
{
    [TestFixture]
    public class ComponentLocatorTest
    {
        [Test]
        public void resolve_component_using_component_locator()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var componentLocator = windsorEngine.GetComponent<IComponentLocator>();

            // ACT
            var component = componentLocator.GetComponent<ITransientComponentMock>();

            // ASSERT
            Assert.That(component, Is.Not.Null);
        }

        [Test]
        public void resolve_stateful_component_using_component_locator()
        {
            // ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var componentLocator = windsorEngine.GetComponent<IComponentLocator>();
            var id = "1234";
            var state = new State(id);

            // ACT
            var component = componentLocator.GetComponent<IStatefulComponent>(new {state});

            // ASSERT
            Assert.That(component, Is.Not.Null);
            Assert.That(component.Id, Is.EqualTo(id));
            Assert.That(component.Dependency, Is.Not.Null);
        }
    }
}