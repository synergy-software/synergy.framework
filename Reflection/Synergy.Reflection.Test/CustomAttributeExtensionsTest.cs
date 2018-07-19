using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Synergy.Reflection.Test
{
    [TestFixture]
    public class CustomAttributeExtensionsTest
    {
        [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Property,
            AllowMultiple = true)]
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

            public void NotOverridenMethod(object argument)
            {
                throw new NotImplementedException();
            }

            [My("attribute on a class property")]
            public string Property { get; set; }
        }

        private interface IABase
        {

        }

        [My("attribute on an interface")]
        private interface IA : IABase
        {
            [My("attribute on an interface method")]
            void Method();

            [My("attribute on an overriden interface method")]
            void Method(bool overriden);

            [My("attribute on a not overriden interface method")]
            void NotOverridenMethod([Display] object argument);

            [My("attribute on an interface property")]
            string Property { get; set; }
        }

        private interface IQueryDispatcher
        {
            [NotNull, Pure]
            TResult Query<TQuery, TResult>([Display] TQuery command) where TQuery : IA;
        }

        private class QueryDispatcher:IQueryDispatcher
        {
            public TResult Query<TQuery, TResult>(TQuery query)
                where TQuery : IA
            {
                throw new NotImplementedException();
            }
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
            Assert.NotNull(property);

            //ACT
            CategoryAttribute[] attributes = property.GetCustomAttributesBasedOn<CategoryAttribute>();

            //ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(MyAttribute));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on a class property"));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on an interface property"));
        }

        [Test]
        public void InheritedAttributeCanBeTakenDirectlyFromInterfaceMethod()
        {
            //ARRANGE
            var method = typeof(IA).GetMethod(nameof(IA.NotOverridenMethod));
            Assert.NotNull(method);

            //ACT
            CategoryAttribute[] attributes = method.GetCustomAttributesBasedOn<CategoryAttribute>();

            // ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(MyAttribute));
            Assert.IsTrue(attributes.Any(a => a.Name == "attribute on a not overriden interface method"));
        }

        [Test]
        public void InheritedAttributeCanBeTakenFromMethodArgumentPlacedOnAnInterface()
        {
            //ARRANGE
            var method = typeof(IA).GetMethod(nameof(IA.NotOverridenMethod));
            Assert.NotNull(method);
            var argument = method.GetParameters()
                                 .First();

            //ACT
            var attributes = argument.GetCustomAttributesBasedOn<DisplayAttribute>();

            // ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(DisplayAttribute));
            CollectionAssert.IsNotEmpty(attributes);
        }

        [Test]
        public void InheritedAttributeCanBeTakenForClassFromMethodArgumentPlacedOnAnInterface()
        {
            //ARRANGE
            var method = typeof(A).GetMethod(nameof(A.NotOverridenMethod));
            Assert.NotNull(method);
            var argument = method.GetParameters()
                                 .First();

            //ACT
            var attributes = argument.GetCustomAttributesBasedOn<DisplayAttribute>();

            // ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(DisplayAttribute));
            CollectionAssert.IsNotEmpty(attributes);
        }

        [Test]
        public void TakeInheritedAttributeForMethodArgumentFromInterfaceWithDifferentParameterName()
        {
            //ARRANGE
            var method = typeof(QueryDispatcher).GetMethod(nameof(QueryDispatcher.Query));
            Assert.NotNull(method);
            var argument = method.GetParameters()
                                 .First();

            //ACT
            var attributes = argument.GetCustomAttributesBasedOn<DisplayAttribute>();

            // ASSERT
            Assert.NotNull(attributes);
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof(DisplayAttribute));
            CollectionAssert.IsNotEmpty(attributes);
        }
    }
}