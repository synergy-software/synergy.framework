using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailBecauseTest
    {
        [Test]
        public void fail_because_message()
        {
            // ACT
            var exception = Fail.Because("failure description");

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("failure description"));
        }
    }
}