# Synergy.Documentation

`Synergy.Documentation` provides .NET APIs for generating and maintaining project documentation from code. It includes public API listings, type dependency exploration, TODO reports, and a small object model for composing Markdown.

**NuGet:** [Synergy.Documentation](https://www.nuget.org/packages/Synergy.Documentation/)

**License:** MIT

**Target frameworks:** .NET 6, .NET 7, and .NET 8

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

You can provide an `excludePath` predicate to skip matching files or directories, and pass custom `TodoPattern` instances to replace the default patterns. The report links to source files relative to the calling source file.

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

Other elements include code blocks, quotes, tables, links, and images. See the [Markdown example](https://github.com/synergy-software/synergy.framework/blob/master/Documentation/Synergy.Documentation.Tests/Markup/MarkdownTests.cs) for a complete sample.

## Examples and tests

The [test project](https://github.com/synergy-software/synergy.framework/tree/master/Documentation/Synergy.Documentation.Tests) contains executable examples of [API listings](https://github.com/synergy-software/synergy.framework/blob/master/Documentation/Synergy.Documentation.Tests/Architecture/Public/Api.cs), [dependency listings](https://github.com/synergy-software/synergy.framework/blob/master/Documentation/Synergy.Documentation.Tests/Architecture/Dependencies/Relations.cs), and [TODO reports](https://github.com/synergy-software/synergy.framework/blob/master/Documentation/Synergy.Documentation.Tests/Architecture/Debt/Todos.cs). Run the tests from the repository root:

```shell
dotnet test Documentation/Synergy.Documentation.Tests/Synergy.Documentation.Tests.csproj
```

For source annotations and fluent comments, see the [Synergy.Documentation.Annotations guide](https://github.com/synergy-software/synergy.framework/blob/master/Documentation/Synergy.Documentation.Annotations/README.md).
