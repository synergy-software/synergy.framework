using Synergy.Documentation.Code;
using Synergy.Documentation.Todos;

namespace Synergy.Web.Api.Tests.Architecture.Debt;

[UsesVerify]
public class Todos
{
    [Fact]
    public async Task Generate()
    {
        var rootFolder = CodeFolder.Current()
                                   .Up(3);
        var technicalDebt = TodoExplorer.DebtFor("Synergy.Web.Api.Testing", rootFolder);

        await Verifier
              .Verify(technicalDebt, "md")
              .UseMethodName("Technical.Debt");
    }
}