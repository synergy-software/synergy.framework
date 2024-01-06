# Synergy.Documentation nuget package

## Enlisting Public API

To enlist public API, we use the following tool:

```csharp
using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Architecture.Public;

[UsesVerify]
public class Api
{
    [Fact]
    public async Task Generate()
    {
        var assembly = typeof(ApiDescription).Assembly;
        var publicApi = ApiDescription.GenerateFor(assembly);
        
        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + assembly.GetName().Name);
    }
}
```

For sample code, please check: [Api.cs](Synergy.Documentation.Tests/Architecture/Public/Api.cs)

To see the results, please check: [Api.of.Synergy.Documentation.verified.md](Synergy.Documentation.Tests/Architecture/Public/Api.of.Synergy.Documentation.verified.md)

**Note:**

When you expose your public API, you should be aware how it looks like from the outside - [Api.cs](Synergy.Documentation.Tests/Architecture/Public/Api.cs).

When you consume some external library - you can enlist its public API and see how it looks like - [Package.cs](Synergy.Documentation.Tests/Architecture/Public/Package.cs).

## Managing technical debt

To manage technical debt, we use the following tool:

```csharp
using Synergy.Documentation.Code;
using Synergy.Documentation.Todos;

namespace Synergy.Documentation.Tests.Architecture.Debt;

[UsesVerify]
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
```

For sample code, please check: [Todos.cs](Synergy.Documentation.Tests/Architecture/Debt/Todos.cs)

To see the results, please check: [Todos.Technical.Debt.verified.md](Synergy.Documentation.Tests/Architecture/Debt/Todos.Technical.Debt.verified.md)

**Note:** 

Basic idea of using this test is to have a list of all technical debt in the project.
Each time developer adds a new technical debt, he should re-run this test otherwise it will fail on the CI.
When all tech debt for the project is materialized in single file - we can start working on it.
It also helps to keep track of all tech debt in the project.
Moreover, it is much easier to spot new technical debt during the code review.

## Enlisting dependencies of a class

To document dependencies of a specific class, we use the following tool:

```csharp
using Synergy.Documentation.Api;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Architecture.Dependencies;

[UsesVerify]
public class Relations
{
    [Theory]
    [InlineData(typeof(Markdown))]
    public async Task Generate(Type type)
    {
        var dependencies = Synergy.Documentation.Api.Dependencies.Of(type, includeNested: true);
        var publicApi = ApiDescription.GenerateFor(dependencies);

        await Verifier.Verify(publicApi, "md")
                      .UseMethodName("of." + type.Name);
    }
}
```

For sample code, please check: [Relations.cs](Synergy.Documentation.Tests/Architecture/Dependencies/Relations.cs)

To see the results, please check: [Relations.of.Markdown.verified.md](Synergy.Documentation.Tests/Architecture/Dependencies/Relations.of.Markdown.verified.md)

## Generating markdown files from code - Docs as Code

TBC

[//]: # (TODO Write the documentation of Markdown class usage)

