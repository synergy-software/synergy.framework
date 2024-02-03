using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailCastTest
    {
        #region object.AsOrFail<T>()

        [Test]
        public void AsOrFail()
        {
            // ARRANGE
            var someObjectButSurelyNotString = new object();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => someObjectButSurelyNotString.AsOrFail<string>()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected someObjectButSurelyNotString of type 'System.String' but was 'System.Object'"));
        }

        [Test]
        public void AsOrFailSuccess()
        {
            // ARRANGE 
            object somethingCastable = "text";

            // ACT
            somethingCastable.AsOrFail<string>();
        }

        [Test]
        public void AsOrFailSuccessWithNull()
        {
            // ARRANGE 
            object somethingCastable = null;

            // ACT
            // ReSharper disable once ExpressionIsAlwaysNull
            somethingCastable.AsOrFail<string>();
        }

        #endregion

        #region object.CastOrFail<T>()

        [Test]
        public void CastOrFail()
        {
            // ARRANGE
            // ReSharper disable once HeapView.BoxingAllocation
            object somethingNotCastable = 1;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => somethingNotCastable.CastOrFail<string>()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected object of type 'System.String' but was '1'"));
        }

        [Test]
        public void CastOrFailWithNull()
        {
            // ARRANGE
            object somethingNotCastable = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => somethingNotCastable.CastOrFail<string>()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected object of type 'System.String' but was 'null'"));
        }

        [Test]
        public void CastOrFailSuccess()
        {
            // ARRANGE
            var somethingCastable = "text";

            // ACT
            somethingCastable.CastOrFail<string>();
        }

        #endregion

        #region Fail.IfNotCastable<T>()

        [Test]
        public void IfNotCastable()
        {
            // ARRANGE
            var somethingNotCastable = new object();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotCastable<IQueryable>(somethingNotCastable, Violation.Of("wrong type"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("wrong type"));
        }

        [Test]
        public void IfNotCastableWithNull()
        {
            Fail.IfNotCastable<IList<string>>(null, Violation.Of("wrong type"));
        }

        [Test]
        public void IfNotCastableSuccess()
        {
            Fail.IfNotCastable<IList<string>>(new List<string>(), Violation.Of("wrong type"));
        }

        #endregion

        #region Fail.IfNotCastable()

        [Test]
        public void WeaklyTypedIfNotCastable()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotCastable(new object(), typeof(IQueryable), Violation.Of("wrong type"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("wrong type"));
        }

        [Test]
        public void WeaklyTypedIfNotCastableWithNull()
        {
            Fail.IfNotCastable(null, typeof(IList<string>), Violation.Of("wrong type"));
        }

        [Test]
        public void WeaklyTypedIfNotCastableSuccess()
        {
            Fail.IfNotCastable(new List<string>(), typeof(IList<string>), Violation.Of("wrong type"));
        }

        #endregion

        #region Fail.IfNullOrNotCastable<T>()

        [Test]
        public void IfNullOrNotCastable()
        {
            // ARRANGE
            object somethingNotCastable = new object();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrNotCastable<IQueryable>(somethingNotCastable)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected object of type 'System.Linq.IQueryable' but was 'System.Object'"));
        }

        [Test]
        public void IfNullOrNotCastableWithMessage()
        {
            // ARRANGE
            object somethingNotCastable = new object();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrNotCastable<IQueryable>(somethingNotCastable, Violation.Of("wrong type"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("wrong type"));
        }

        [Test]
        public void IfNullOrNotCastableWithNull()
        {
            // ARRANGE
            object somethingNotCastable = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => Fail.IfNullOrNotCastable<IQueryable>(somethingNotCastable)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected object of type 'System.Linq.IQueryable' but was 'null'"));
        }

        [Test]
        public void IfNullOrNotCastableSuccess()
        {
            Fail.IfNullOrNotCastable<IList<string>>(new List<string>());
        }

        [Test]
        public void IfNullOrNotCastableSuccessWithMessage()
        {
            Fail.IfNullOrNotCastable<IList<string>>(new List<string>(), Violation.Of("wrong type"));
        }

        #endregion
    }
}