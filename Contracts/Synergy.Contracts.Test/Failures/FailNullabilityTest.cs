using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void FailIfNull(object someNullObject)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => someNullObject.FailIfNull(nameof(someNullObject))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'someNullObject' is null; and it shouldn't be;"));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void FailIfNullWithViolationMessage(object someNullObject)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => someNullObject.FailIfNull(Violation.Of("this is null: {0}", nameof(someNullObject)))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this is null: someNullObject"));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void FailIfNullSuccess(object thisIsNotNull)
        {
            // ACT
            thisIsNotNull.FailIfNull(nameof(thisIsNotNull));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void FailIfNullWithViolationMessageSuccess(object thisIsNotNull)
        {
            // ACT
            thisIsNotNull.FailIfNull(Violation.Of("{0} should not be null", nameof(thisIsNotNull)));
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
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void OrFail(object thisMustBeNull)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => thisMustBeNull.OrFail(nameof(thisMustBeNull)));

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'thisMustBeNull' is null; and it shouldn't be;"));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public void OrFailSuccess(object thisCannotBeNull)
        {
            // ACT
            Type type = thisCannotBeNull.OrFail(nameof(thisCannotBeNull)).GetType();
        }

        #endregion
        
        #region nullable.OrFail()

        [Test]
        public void NullableOrFail()
        {
            // ARRANGE
            int? thisMustBeNull = null; 
            
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => thisMustBeNull.OrFail(nameof(thisMustBeNull)));

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'thisMustBeNull' is null; and it shouldn't be;"));
        }

        [Test]
        public void NullableOrFailSuccess()
        {
            // ARRANGE
            int? thisCannotBeNull = 3;

            // ACT
            thisCannotBeNull.OrFail(nameof(thisCannotBeNull));
        }

        #endregion

        #region variable.NotNull()

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void NotNull(object thisMustBeNull)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => thisMustBeNull.NotNull(nameof(thisMustBeNull)));

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'thisMustBeNull' is null; and it shouldn't be;"));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void NotNullSuccess(object thisCannotBeNull)
        {
            // ACT
            thisCannotBeNull.NotNull(nameof(thisCannotBeNull));
        }

        #endregion

        #region variable.CanBeNull()

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void CanBeNull(object value)
        {
            // ACT
            var result = value.CanBeNull();

            // ASSERT
            Assert.That(value, Is.EqualTo(result));
        }

        #endregion

        #region Fail.IfArgumentNull

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void IfArgumentNull([CanBeNull] object argumentValue)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNull(argumentValue, nameof(argumentValue))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'argumentValue' is null."));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void IfArgumentNullSuccess(object argumentValue)
        {
            // ACT
            Fail.IfArgumentNull(argumentValue, nameof(argumentValue));
        }

        #endregion

        #region Fail.IfNotNull

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void IfNotNull(object argumentValue)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotNull(argumentValue, nameof(argumentValue))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'argumentValue' is NOT null; and it should be;"));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void IfNotNullSuccess([CanBeNull] object argumentValue)
        {
            // ACT
            Fail.IfNotNull(argumentValue, nameof(argumentValue));
        }

        #endregion

        #region Fail.IfNull

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void IfNullWithName(object thisIsNull)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => Fail.IfNull(thisIsNull, nameof(thisIsNull))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'thisIsNull' is null; and it shouldn't be;"));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNulls))]
        public void IfNullWithMessage(object thisIsNull)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                // ReSharper disable once HeapView.BoxingAllocation
                () => Fail.IfNull(thisIsNull, Violation.Of("this is null and it shouldn't be {0} {1} {2} {3}", 1, "never", "maybe", "wow"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this is null and it shouldn't be 1 never maybe wow"));
        }

        [Test]
        [TestCaseSource(nameof(FailNullabilityTest.GetNotNulls))]
        public void IfNullSuccess(object argumentValue)
        {
            // ACT
            Fail.IfNull(argumentValue, nameof(argumentValue));
        }

        #endregion

        private static IEnumerable<object> GetNotNulls()
        {
            // ReSharper disable HeapView.BoxingAllocation
            yield return 123;
            yield return (long?) 456;
            yield return new object();
            yield return "";
            // ReSharper restore HeapView.BoxingAllocation
        }

        private static IEnumerable<object?> GetNulls()
        {
            // ReSharper disable HeapView.BoxingAllocation
            yield return null;
            //yield return (long?) null;
            // ReSharper restore HeapView.BoxingAllocation
        }
    }
}