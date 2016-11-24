using System;
using System.Linq;
using NUnit.Framework;

namespace Synergy.Reflection.Test
{
    [TestFixture]
    public class CustomAttributeExtensionsTest
    {
        [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
        private class MyAttribute : CategoryAttribute
        {
            public MyAttribute(string name) : base(name)
            {
            }
        }

        [My("attribute on a class")]
        private class A : IA
        {
            [My("attribute on a class method")]
            public void Method()
            {
            }

            [My("attribute on an overriden class method")]
            public void Method(bool overriden)
            {
                throw new InvalidOperationException();
            }

            [My("attribute on a class property")]
            public string Property { get; set; }
        }

        [My("attribute on an interface")]
        private interface IA
        {
            [My("attribute on an interface method")]
            void Method();

            [My("attribute on an overriden interface method")]
            void Method(bool overriden);

            [My("attribute on an interface property")]
            string Property { get; set; }
        }

        [Test]
        public void InheritedAttributeCanBeTakenFromClassOrInterface()
        {
            //ARRANGE
            Type type = typeof(A);

            //ACT
            CategoryAttribute[] attributes = type.GetCustomAttributesBasedOn<CategoryAttribute>();

            //ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(MyAttribute));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on a class"));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on an interface"));
        }

        [Test]
        public void InheritedAttributeCanBeTakenFromMethodOrInterface()
        {
            //ARRANGE
            var method = typeof(A).GetMethods()
                                  .First(m => m.Name == nameof(IA.Method));

            //ACT
            CategoryAttribute[] attributes = method.GetCustomAttributesBasedOn<CategoryAttribute>();

            //ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(MyAttribute));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on a class method"));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on an interface method"));

            Assert.IsFalse(attributes.Any(a => a.Name == "attribute on an overriden class method"));
            Assert.IsFalse(attributes.Any(a => a.Name == "attribute on an overriden interface method"));
        }

        [Test]
        public void InheritedAttributeCanBeTakenFromPropertyOrInterface()
        {
            //ARRANGE
            var property = typeof(A).GetProperty(nameof(IA.Property));

            //ACT
            CategoryAttribute[] attributes = property.GetCustomAttributesBasedOn<CategoryAttribute>();

            //ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(MyAttribute));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on a class property"));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on an interface property"));
        }
    }
}