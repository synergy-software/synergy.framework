using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailNullabilityTest
    {
        #region variable.FailIfNull(nameof(variable))

        [Test]
        public void FailIfNull()
        {
            // ARRANGE
            object someNullObject = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => someNullObject.FailIfNull("'{0}' is null and it shouldn't be", nameof(someNullObject))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'someNullObject' is null and it shouldn't be"));
        }

        [Test]
        public void FailIfNullOnNullableValue()
        {
            // ARRANGE
            long? someNullableLong = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => someNullableLong.FailIfNull("'{0}' is null and it shouldn't be", nameof(someNullableLong))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'someNullableLong' is null and it shouldn't be"));
        }

        [Test]
        public void FailIfNullSuccess()
        {
            // ARRANGE
            var thisIsNotNull = new object();

            // ACT
            thisIsNotNull.FailIfNull(nameof(thisIsNotNull));

            long? itIsNotNull = 123;
            itIsNotNull.FailIfNull(nameof(itIsNotNull));
        }

        [Test]
        public void FailIfNullSuccessOnNullableValue()
        {
            // ARRANGE
            long? thisIsNotNull = 123;

            // ACT
            thisIsNotNull.FailIfNull(nameof(thisIsNotNull));
        }

        [Test]
        public void FailIfNullSample()
        {
            // ARRANGE
            IContractorRepository repository = new ContractorRepository();
            var parameters = new ContractorFilterParameters();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => repository.FilterContractors(parameters)
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("'EstablishedBetween' is null and it shouldn't be"));
        }

        #endregion

        #region variable.OrFail()

        [Test]
        public void OrFail()
        {
            // ARRANGE
            string thisMustBeNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => thisMustBeNull.OrFail(nameof(thisMustBeNull)));

            // ASSERT
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void OrFailSuccess()
        {
            // ARRANGE
            var thisCannotBeNull = "i am not null";

            // ACT
            thisCannotBeNull.OrFail(nameof(thisCannotBeNull));
        }

        #endregion

        #region variable.NotNull()

        [Test]
        public void NotNull()
        {
            // ARRANGE
            string thisMustBeNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => thisMustBeNull.NotNull(nameof(thisMustBeNull)));

            // ASSERT
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void NotNullSuccess()
        {
            // ARRANGE
            var thisCannotBeNull = "i am not null";

            // ACT
            thisCannotBeNull.NotNull(nameof(thisCannotBeNull));
        }

        #endregion

        #region variable.OrFail()

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetMixOfNullsAndNotNulls))]
        public void CanBeNull(object value)
        {
            // ACT
            var result = value.CanBeNull();

            // ASSERT
            Assert.That(value, Is.EqualTo(result));
        }

        [ItemCanBeNull]
        private static IEnumerable<object> GetMixOfNullsAndNotNulls()
        {
            // ReSharper disable HeapView.BoxingAllocation
            yield return 123;
            yield return (long?)456;
            yield return new object();
            yield return null;
            // ReSharper restore HeapView.BoxingAllocation
        }

        #endregion

        #region Fail.IfArgumentNull

        [Test]
        [TestCase(null)]
        public void IfArgumentNull([CanBeNull] object argumentValue)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNull(argumentValue, nameof(argumentValue))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'argumentValue' was null."));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void IfArgumentNullSucces([NotNull] object argumentValue)
        {
            // ACT
            Fail.IfArgumentNull(argumentValue, nameof(argumentValue));
        }

        [ItemNotNull]
        private static IEnumerable<object> GetNotNulls()
        {
            // ReSharper disable HeapView.BoxingAllocation
            yield return 123;
            yield return (long?) 456;
            yield return new object();
            // ReSharper restore HeapView.BoxingAllocation
        }

        #endregion

        #region Fail.IfNotNull

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void IfNotNull(object argumentValue)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotNull(argumentValue, "value is NOT null - something went wrong")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("value is NOT null - something went wrong"));
        }

        [Test]
        [TestCase(null)]
        public void IfNotNullSucces([CanBeNull] object argumentValue)
        {
            // ACT
            Fail.IfNotNull(argumentValue, "value is NOT null - something went wrong");
        }

        #endregion

        #region Fail.IfNull

        [Test]
        public void IfNullWith0Arguments()
        {
            // ARRANGE
            object thisIsNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => Fail.IfNull(thisIsNull, "this is null and it shouldn't be")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this is null and it shouldn't be"));
        }

        [Test]
        public void IfNullWith1Arguments()
        {
            // ARRANGE
            object thisIsNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => Fail.IfNull(thisIsNull, "this is null and it shouldn't be {0}", 1)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this is null and it shouldn't be 1"));
        }

        [Test]
        public void IfNullWith2Arguments()
        {
            // ARRANGE
            object thisIsNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => Fail.IfNull(thisIsNull, "this is null and it shouldn't be {0} {1}", 1, "never")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this is null and it shouldn't be 1 never"));
        }

        [Test]
        public void IfNullWith3Arguments()
        {
            // ARRANGE
            object thisIsNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => Fail.IfNull(thisIsNull, "this is null and it shouldn't be {0} {1} {2}", 1, "never", "maybe")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this is null and it shouldn't be 1 never maybe"));
        }

        [Test]
        public void IfNullWithNArguments()
        {
            // ARRANGE
            object thisIsNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                // ReSharper disable once HeapView.BoxingAllocation
                () => Fail.IfNull(thisIsNull, "this is null and it shouldn't be {0} {1} {2} {3}", 1, "never", "maybe", "wow")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this is null and it shouldn't be 1 never maybe wow"));
        }

        [Test]
        public void IfNullSuccess()
        {
            // ARRANGE
            object thisIsNotNull = "not null";

            // ACT
            Fail.IfNull(thisIsNotNull, "this is null and it shouldn't be");
        }

        [Test]
        public void IfNullForNotNullableType()
        {
            // ARRANGE
            long thisIsNotNull = 123;

            // ACT
            Fail.IfNull(thisIsNotNull, "this is null and it shouldn't be");
        }

        #endregion
    }
}