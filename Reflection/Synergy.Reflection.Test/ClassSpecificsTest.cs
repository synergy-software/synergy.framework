using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Synergy.Reflection.Test
{
    public class SomeClass
    {
        [UsedImplicitly]
        private string field1 = "field 1 value";

        public const string Field1Name = nameof(SomeClass.field1);
    }

    [TestFixture]
    public class ClassSpecificsTest
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        private string Property1 { get; }

        [Display(Name="Property with display name")]
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        private string Property2 { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        private ClassSpecificsTest SubTest { get; }

        [Test]
        public void Off()
        {
            // ARRANGE
            var type = this.GetType();

            // ACT
            var assemblyeSpecifics = ClassSpecifics.Of(type);

            // ASSERT
            Assert.That(assemblyeSpecifics, Is.Not.Null);
        }

        [Test]
        public void GetPropertyName()
        {
            // ARRANGE
            var classSpecifics = ClassSpecifics.Of<ClassSpecificsTest>();

            // ACT
            var propertyName = classSpecifics.GetPropertyName(test => test.Property1);

            // ASSERT
            Assert.That(propertyName, Is.EqualTo("Property1"));
        }

        [Test]
        public void GetPropertyDisplayNameForPropertyWithoutDisplayAttribute()
        {
            // ARRANGE
            var classSpecifics = ClassSpecifics.Of<ClassSpecificsTest>();

            // ACT
            var propertyDisplayName = classSpecifics.GetPropertyDisplayName(test => test.Property1);

            // ASSERT
            Assert.That(propertyDisplayName, Is.Null);
        }

        [Test]
        public void GetPropertyDisplayName()
        {
            // ARRANGE
            var classSpecifics = ClassSpecifics.Of<ClassSpecificsTest>();

            // ACT
            var propertyDisplayName = classSpecifics.GetPropertyDisplayName(test => test.Property2);

            // ASSERT
            Assert.That(propertyDisplayName, Is.EqualTo("Property with display name"));
        }

        [Test]
        public void GetPropertyInfo()
        {
            // ARRANGE
            var classSpecifics = ClassSpecifics.Of<ClassSpecificsTest>();

            // ACT
            var propertyDisplayName = classSpecifics.GetPropertyInfo(test => test.Property2);

            // ASSERT
            Assert.That(propertyDisplayName, Is.Not.Null);
        }

        [Test]
        public void GetPropertyPath()
        {
            // ARRANGE
            var classSpecifics = ClassSpecifics.Of<ClassSpecificsTest>();

            // ACT
            var propertyDisplayName = classSpecifics.GetPropertyPath(test => test.Property2);

            // ASSERT
            Assert.That(propertyDisplayName, Is.EqualTo("Property2"));
        }

        [Test]
        public void GetPropertyPathForDeepPath()
        {
            // ARRANGE
            var classSpecifics = ClassSpecifics.Of<ClassSpecificsTest>();

            // ACT
            var propertyDisplayName = classSpecifics.GetPropertyPath(test => test.SubTest.Property1);

            // ASSERT
            Assert.That(propertyDisplayName, Is.EqualTo("SubTest.Property1"));
        }

        [Test]
        public void GetField()
        {
            // ARRANGE
            var someClass = ClassSpecifics.Of<SomeClass>();

            // ACT
            var field = someClass.GetFieldInfo(SomeClass.Field1Name);

            // ASSERT
            Assert.That(field, Is.Not.Null);
        }
    }
}
