using Synergy.Documentation.Annotations;
using Synergy.Documentation.Code;
using Synergy.Documentation.Todos;

namespace Synergy.Documentation.Tests.Architecture.Debt;

[UsesVerify]
[CodeFilePath]
public class Todos
{
    [Fact]
    public async Task Generate()
    {
        var rootFolder = CodeFolder.Current().Up(3);
        var technicalDebt = TodoExplorer.DebtFor("Synergy.Documentation", rootFolder);

        await Verifier
              .Verify(technicalDebt, "md")
              .UseMethodName("Technical.Debt");
    }
}