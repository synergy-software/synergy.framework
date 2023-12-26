# Synergy.Documentation nuget package

## Managing technical debt

To manage technical debt, we use the following tool:

```csharp
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
```

For details, please check:[Todos.cs](Synergy.Documentation.Tests/Architecture/Debt/Todos.cs)

## Managing dependencies


