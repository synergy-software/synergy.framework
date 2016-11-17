using NUnit.Framework;
using Synergy.Pooling;

namespace Synergy.Extensions.Test
{
    [TestFixture]
    public class PoolTest
    {
        [Test]
        public void exhaust_the_pool()
        {
            //ARRANGE
            // ReSharper disable once RedundantArgumentDefaultValue
            var pool = new Pool<object>(() => new object(), initialSize: 1);

            //ACT
            using (Pooled<object> object1 = pool.Get())
            {
                using (Pooled<object> object2 = pool.Get())
                {
                    //ASSERT
                    Assert.That(object1, Is.Not.EqualTo(object2));
                    Assert.That(object1.Value, Is.Not.EqualTo(object2.Value));
                }
            }
        }
    }
}