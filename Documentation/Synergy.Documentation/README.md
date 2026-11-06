# Synergy.Documentation

`Synergy.Documentation` provides .NET APIs for generating and maintaining project documentation from code. It includes public API listings, type dependency exploration, TODO reports, and a small object model for composing Markdown.

**NuGet:** [Synergy.Documentation](https://www.nuget.org/packages/Synergy.Documentation/)

**License:** MIT

**Target frameworks:** .NET 8 and .NET 10

## Install

```shell
dotnet add package Synergy.Documentation
```

The package depends on `Synergy.Documentation.Annotations`. Install that package directly if you only need its annotations and fluent comment helpers.

## Generate a public API listing

`ApiDescription.GenerateFor` reflects over an assembly and returns a Markdown-formatted description of its public types and members:

```csharp
using System.IO;
using Synergy.Documentation.Api;

var markdown = ApiDescription.GenerateFor(typeof(MyService).Assembly);
File.WriteAllText("public-api.md", markdown);
```

Pass `includeAssemblyVersion: true` to include the assembly version in the heading. The result is a string; choose the file location and whether to compare it with a checked-in snapshot in your own build or test workflow.

**Example test:** [Api.Generate](../Synergy.Documentation.Tests/Architecture/Public/Api.cs).
**Results:** [Documentation API listing](../Synergy.Documentation.Tests/Architecture/Public/Api.of.Synergy.Documentation.verified.md) and [Annotations API listing](../Synergy.Documentation.Tests/Architecture/Public/Api.of.Synergy.Documentation.Annotations.verified.md).
The [Package.Generate test](../Synergy.Documentation.Tests/Architecture/Public/Package.cs) demonstrates including assembly versions: [Verify.Xunit result](../Synergy.Documentation.Tests/Architecture/Public/Package.of.Verify.Xunit.verified.md) and [xunit.core result](../Synergy.Documentation.Tests/Architecture/Public/Package.of.xunit.core.verified.md).

## Explore type dependencies

`Dependencies.Of` starts from a type and returns the types found through its public properties. Set `includeNested: true` to include its public nested types:

```csharp
using System.IO;
using Synergy.Documentation.Api;

var types = Dependencies.Of(typeof(MyService), includeNested: true);
var markdown = ApiDescription.GenerateFor(types);
File.WriteAllText("my-service-dependencies.md", markdown);
```

This is a reflection-based view of property types, not a complete runtime or project dependency graph.

**Example test:** [Relations.Generate](../Synergy.Documentation.Tests/Architecture/Dependencies/Relations.cs).
**Result:** [Markdown type dependency listing](../Synergy.Documentation.Tests/Architecture/Dependencies/Relations.of.Markdown.verified.md).

## Collect TODOs

`TodoExplorer.DebtFor` scans a folder recursively using its built-in TODO patterns and returns a Markdown report:

```csharp
using System.IO;
using Synergy.Documentation.Code;
using Synergy.Documentation.Todos;

var source = new CodeFolder(Path.Combine("src", "MyProject"));
var report = TodoExplorer.DebtFor("MyProject", source);
File.WriteAllText("technical-debt.md", report);
```

In Markdown files, a TODO can be written as a `[//]: #` comment followed by `(TODO ...)`, or as an HTML comment that starts a line, `<!-- TODO ... -->`. Use the HTML comment when the message needs parentheses or Markdown links: the first form ends at the first `)`.

You can provide an `excludePath` predicate to skip matching files or directories, and pass custom `TodoPattern` instances to replace the default patterns. The report links to source files relative to the calling source file.

**Example test:** [Todos.Generate](../Synergy.Documentation.Tests/Architecture/Debt/Todos.cs).
**Result:** [Technical debt report](../Synergy.Documentation.Tests/Architecture/Debt/Todos.Technical.Debt.verified.md).

## Compose Markdown

The `Markdown.Document` API lets you append supported Markdown elements and render the document to a string:

```csharp
using System.IO;
using Synergy.Documentation.Markup;

var document = new Markdown.Document()
    .Append(new Markdown.Header1("Release notes"))
    .Append(new Markdown.Header2("Added"))
    .Append(new Markdown.Paragraph("API documentation is now generated."));

File.WriteAllText("release-notes.md", document.ToString());
```

Other elements include code blocks, quotes, tables, links, and images.

**Example test:** [MarkdownTests.CreateMarkdownDocument](../Synergy.Documentation.Tests/Markup/MarkdownTests.cs).
**Result:** [Rendered Markdown document](../Synergy.Documentation.Tests/Markup/MarkdownTests.CreateMarkdownDocument.verified.md).

`new Markdown.Quote(null)` creates an empty quote that can be populated with
`Line`. API and property-dependency listings also support types in the global
namespace. When using `ClassDocumentation`, missing XML summaries are omitted
from the type description or rendered as empty method-summary cells; errors
reading or parsing the XML documentation file are still propagated.

## Examples and tests

Each feature section above links to an executable test and its checked-in result. The `.verified.md` files are expected outputs compared by Verify, not additional files written by the README snippets. Run the [test project](../Synergy.Documentation.Tests) from the repository root:

```shell
dotnet test Documentation/Synergy.Documentation.Tests/Synergy.Documentation.Tests.csproj
```

For source annotations and fluent comments, see the [Synergy.Documentation.Annotations guide](../Synergy.Documentation.Annotations/README.md).
