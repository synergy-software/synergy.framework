using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailEqualityTest
    {
        #region Fail.IfEqual()

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetEquals))]
        public void IfEqualWithMessage(Pair obj)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual(obj.Value1, obj.Value2, Violation.Of("{0} is equal to {1} and shouldn't {2}. {3}", "first", "second", "be", "Seriously?"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("first is equal to second and shouldn't be. Seriously?"));
        }

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetNotEquals))]
        public void IfEqualSuccess(Pair obj)
        {
            // ACT
            Fail.IfEqual(obj.Value1, obj.Value2, "values are equal and shouldn't be");
        }

        #endregion

        #region Fail.IfArgumentEqual()

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetEquals))]
        public void IfArgumentEqual(Pair obj)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEqual(obj.Value1, obj.Value2, nameof(obj))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'obj' is equal to " + obj.GetValue2() + " and it should NOT be."));
        }

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetNotEquals))]
        public void IfArgumentEqualSuccess(Pair obj)
        {
            // ACT
            Fail.IfArgumentEqual(obj.Value1, obj.Value2, nameof(obj));
        }

        #endregion

        #region Fail.IfNotEqual()

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetNotEquals))]
        public void IfNotEqualWithMessage(Pair obj)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotEqual(obj.Value1, obj.Value2, Violation.Of("values differ and should be equal"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("values differ and should be equal"));
        }

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetEquals))]
        public void IfNotEqualWithMessageSuccess(Pair obj)
        {
            // ACT
            Fail.IfNotEqual(obj.Value1, obj.Value2, Violation.Of("values differ and should be equal"));
        }

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetNotEquals))]
        public void IfNotEqualWithName(Pair obj)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotEqual(obj.Value2, obj.Value1, nameof(obj))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'obj' is NOT equal to " + obj.GetValue2() + " and it should be."));
        }

        [Test]
        [TestCaseSource(nameof(FailEqualityTest.GetEquals))]
        public void IfNotEqualWithNameSuccess(Pair obj)
        {
            // ACT
            Fail.IfNotEqual(obj.Value1, obj.Value2, nameof(obj));
        }

        #endregion

        private static IEnumerable<Pair> GetEquals()
        {
            yield return new Pair(1, 1);
            yield return new Pair(null, null);
        }

        private static IEnumerable<Pair> GetNotEquals()
        {
            yield return new Pair(1, 2);
            yield return new Pair(1, 1.0M);
            yield return new Pair(new object(), null);
        }

        public struct Pair
        {
            public readonly object Value1;
            public readonly object Value2;

            public Pair(object v1, object v2)
            {
                this.Value1 = v1;
                this.Value2 = v2;
            }

            [NotNull] public string GetValue2() => this.Value2 == null ? "null" : this.Value2.ToString();
            public override string ToString() => $"{this.Value1}, {this.GetValue2()}";
        }
    }
}