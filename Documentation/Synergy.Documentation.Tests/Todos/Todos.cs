using Synergy.Documentation.Code;
using Synergy.Documentation.Todos;

namespace Synergy.Documentation.Tests.Todos;

[UsesVerify]
public class Todos
{
    [Fact]
    public async Task Generate()
    {
        var rootFolder = CodeFolder.Current()
                                   .Up(2);
        var technicalDebt = TodoExplorer.DebtFor("Synergy.Contracts", rootFolder);

        await Verifier
              .Verify(technicalDebt, "md")
              .UseMethodName("Technical.Debt");
    }
}