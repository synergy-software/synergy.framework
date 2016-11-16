using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace Synergy.Reflection.Test
{
    [TestFixture]
    public class EnumSpecificsTest
    {
        private enum TestEnum
        {
            Value1 = 100,

            [Display(Name = "Display name of Value2")]
            [EnumMember(Value = "ValueOfEnum2")]
            Value2 = 300
        }

        [Test]
        public void GetEnumName()
        {
            // ARRANGE
            var enumSpecifics = EnumSpecifics.Of<TestEnum>();

            // ACT
            var name = enumSpecifics.GetEnumName(TestEnum.Value1);

            // ASSERT
            Assert.That(name, Is.EqualTo("Value1"));
        }

        [Test]
        public void GetEnumNameForEnumWithEnumMemberAttribute()
        {
            // ARRANGE
            var enumSpecifics = EnumSpecifics.Of<TestEnum>();

            // ACT
            var name = enumSpecifics.GetEnumName(TestEnum.Value2);

            // ASSERT
            Assert.That(name, Is.EqualTo("ValueOfEnum2"));
        }

        [Test]
        public void GetEnumDisplayName()
        {
            // ARRANGE
            var enumSpecifics = EnumSpecifics.Of<TestEnum>();

            // ACT
            var displayName = enumSpecifics.GetEnumDisplayName(TestEnum.Value2);

            // ASSERT
            Assert.That(displayName, Is.EqualTo("Display name of Value2"));
        }

        [Test]
        public void GetEnumDisplayNameWhenThereIsNoDisplayAttribute()
        {
            // ARRANGE
            var enumSpecifics = EnumSpecifics.Of<TestEnum>();

            // ACT
            var displayName = enumSpecifics.GetEnumDisplayName(TestEnum.Value1);

            // ASSERT
            Assert.That(displayName, Is.Null);
        }

        [Test]
        public void GetValues()
        {
            // ARRANGE
            var enumSpecifics = EnumSpecifics.Of<TestEnum>();

            // ACT
            var values = enumSpecifics.GetValues();

            // ASSERT
            Assert.That(values, Is.EquivalentTo(new [] { TestEnum .Value1, TestEnum.Value2 }));
        }
    }
}
