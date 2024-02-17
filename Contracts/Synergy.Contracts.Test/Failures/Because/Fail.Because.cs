using Synergy.Contracts.Samples.Domain;
using Xunit;

namespace Synergy.Contracts.Test.Failures.Because;

public class FailBecauseTest
{
    [Fact]
    public void BecauseWith0Arguments()
    {
        // ACT
        var exception = Fail.Because("Always");

        // ASSERT
        Assert.NotNull(exception);
        Assert.Equal("Always", exception.Message);
    }

    [Fact]
    public void BecauseWith1Argument()
    {
        // ACT
        // ReSharper disable once HeapView.BoxingAllocation
        var exception = Fail.Because("Always {0}", 1);

        // ASSERT
        Assert.NotNull(exception);
        Assert.Equal("Always 1", exception.Message);
    }

    [Fact]
    public void BecauseWith2Arguments()
    {
        // ACT
        // ReSharper disable once HeapView.BoxingAllocation
        var exception = Fail.Because("Always {0} {1}", "fails", 1);

        // ASSERT
        Assert.NotNull(exception);
        Assert.Equal("Always fails 1", exception.Message);
    }

    [Fact]
    public void BecauseWith3Arguments()
    {
        // ACT
        // ReSharper disable once HeapView.BoxingAllocation
        var exception = Fail.Because("Always {0} {1} {2}", "fails", 1, "times");

        // ASSERT
        Assert.NotNull(exception);
        Assert.Equal("Always fails 1 times", exception.Message);
    }

    [Fact]
    public void BecauseWithNArguments()
    {
        // ACT
        // ReSharper disable once HeapView.BoxingAllocation
        var exception = Fail.Because("Always {0} {1} {2} {3}", "fails", 1, "times", "frequently");

        // ASSERT
        Assert.NotNull(exception);
        Assert.Equal("Always fails 1 times frequently", exception.Message);
    }

    [Fact]
    public void BecauseSample()
    {
        // ARRANGE
        var contractor = new Contractor();

        // ACT
        var exception = Assert.Throws<DesignByContractViolationException>(
            () => contractor.SetPersonName("Marcin", "Celej"));

        // ASSERT
        Assert.Equal("Not implemented yet", exception.Message);
    }
}