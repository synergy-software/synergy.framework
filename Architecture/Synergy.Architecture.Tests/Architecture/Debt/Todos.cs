using Synergy.Documentation.Code;
using Synergy.Documentation.Todos;

namespace Synergy.Architecture.Tests.Architecture.Debt;

[UsesVerify]
public class Todos
{
    [Fact]
    public async Task Generate()
    {
        var rootFolder = CodeFolder.Current()
                                   .Up(3);
        var technicalDebt = TodoExplorer.DebtFor("Synergy.Contracts", rootFolder);

        await Verifier
              .Verify(technicalDebt, "md")
              .UseMethodName("Technical.Debt");
    }
}