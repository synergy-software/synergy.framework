using NUnit.Framework;
using Synergy.Core.Windsor;

namespace Synergy.Core.Test.Interceptors
{
    [TestFixture]
    public class InterceptorTest
    {
        [Test]
        public void ComponentShouldBeInteceptedIfRequired()
        {
            //ARRANGE
            IWindsorEngine c = ApplicationServer.Start();
            var intercepted = c.GetComponent<IInterceptedComponent>();
            ComponentInterceptor.WasInvoked = false;

            //ACT
            intercepted.Execute();

            //ASSERT
            Assert.That(ComponentInterceptor.WasInvoked, Is.True);
        }
    }
}