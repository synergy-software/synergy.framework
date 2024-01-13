# Synergy.Documentation nuget package

Here is the documentation of the `Synergy.Documentation` and `Synergy.Documentation.Annotations` nuget packages.
They were created to help developers to document their code in a simple way.
It is based on the idea of [Docs as Code](https://www.writethedocs.org/guide/docs-as-code/).

## Enlisting Public API

To enlist public API, we use the following tool:

```csharp
using Synergy.Documentation.Annotations;
using Synergy.Documentation.Api;

namespace Synergy.Documentation.Tests.Architecture.Public;

[UsesVerify]
[CodeFilePath]
public class Api
{
    [Theory]
    [InlineData(typeof(ApiDescription))]
    [InlineData(typeof(CodeFilePathAttribute))]
    public async Task Generate(Type representative)
    {
        var assembly = representative.Assembly;
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

## Enlisting dependencies of a class

To document dependencies of a specific class, we use the following tool:

```csharp
using Synergy.Documentation.Annotations;
using Synergy.Documentation.Api;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Architecture.Dependencies;

[UsesVerify]
[CodeFilePath]
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

## Managing technical debt

To manage technical debt, we use the following tool:

```csharp
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
```

For sample code, please check: [Todos.cs](Synergy.Documentation.Tests/Architecture/Debt/Todos.cs)

To see the results, please check: [Todos.Technical.Debt.verified.md](Synergy.Documentation.Tests/Architecture/Debt/Todos.Technical.Debt.verified.md)

**Note:** 

Basic idea of using this test is to have a list of all technical debt in the project.
Each time developer adds a new technical debt, he should re-run this test otherwise it will fail on the CI.
When all tech debt for the project is materialized in single file - we can start working on it.
It also helps to keep track of all tech debt in the project.
Moreover, it is much easier to spot new technical debt during the code review.

## Comments as code

You probably wonder why we use this approach instead of using standard comments.
For sure you heard that comments in code are bad because nobody reads them.
Actually they are invisible for developer.

To make them visible, we need to convert them into code.
To use comments as code approach, we use the following tool:

```csharp
using Synergy.Catalogue;
using Synergy.Documentation.Annotations;

namespace Synergy.Documentation.Tests.Comments;

[CodeFilePath]
public class NoteTests
{
    [Fact]
    public void ShowOff()
    {
        this.Comment("Here you have full sample of comments as code")
            .Because("I want to show you how to use them")
            .DoNothing("because this is just a comment")
            .Because("I want to show you how to use them")
            .DoNotThrowException("because this is just a comment")
            .Because("I want to show you how to use them")
            .Then("I want to show you how to use them")
            .But("I want to show you how to use them")
            .Therefore("I want to show you how to use them")
            .Otherwise("I want to show you how to use them")
            .Moreover("I want to show you how to use them")
            .Reference("https://stackoverflow.blog/2021/12/23/best-practices-for-writing-code-comments/");
    }
    
    [Fact]
    public void CommentsTests()
    {
        try
        {
            var list = new List<string>(10.Because("we do not waste memory when we know exact size of the list"));

            if (list.IsEmpty())
                this.DoNothing("because we do not have any items in the list");
        }
        catch
        {
            this.DoNotThrowException("when something bad happens here")
                .Because("this code should always work")
                .Therefore("we should log the exception at least");
        }
    }
}
```

For sample code, please check: [NoteTests.cs](Synergy.Documentation.Tests/Comments/NoteTests.cs)

**Notes:** 

- Do not use this approach to comment something obvious. Do not use ANY comment in such case.
- Use this approach to comment something that is important and you want to explain it to the reader.

To learn more how to write good comments, please check: [Best practices for writing code comments](https://stackoverflow.blog/2021/12/23/best-practices-for-writing-code-comments/):
> - Rule 1: Comments should not duplicate the code.
> - Rule 2: Good comments do not excuse unclear code.
> - Rule 3: If you can't write a clear comment, there may be a problem with the code.
> - Rule 4: Comments should dispel confusion, not cause it.
> - Rule 5: Explain unidiomatic code in comments.
> - Rule 6: Provide links to the original source of copied code.
> - Rule 7: Include links to external references where they will be most helpful.
> - Rule 8: Add comments when fixing bugs.
> - Rule 9: Use comments to mark incomplete implementations.

## Generating markdown files from code - Docs as Code

TBC

[//]: # (TODO Write the documentation of Markdown class usage)

