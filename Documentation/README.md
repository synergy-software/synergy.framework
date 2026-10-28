# Synergy Documentation

Synergy Documentation is a small set of .NET libraries for treating documentation as part of your codebase. Generate Markdown from public APIs, inspect type dependencies, collect TODOs, and compose Markdown documents from C#.

## Choose a package

| Package | Use it for | Documentation |
| --- | --- | --- |
| [`Synergy.Documentation`](https://www.nuget.org/packages/Synergy.Documentation/) | Generating API and dependency listings, collecting TODOs, and creating Markdown documents. | [Package guide](Synergy.Documentation/README.md) |
| [`Synergy.Documentation.Annotations`](https://www.nuget.org/packages/Synergy.Documentation.Annotations/) | Source-level annotations for locating code files and writing explanatory comments as fluent code. | [Package guide](Synergy.Documentation.Annotations/README.md) |

The main package targets .NET 8 and .NET 10. The annotations package additionally targets .NET Standard 2.0, so it can also be used from .NET Framework projects. The annotations package can be used on its own; the main package depends on it.

## Getting started

Install the package that provides the feature you need:

```shell
dotnet add package Synergy.Documentation
```

Or install only the annotations:

```shell
dotnet add package Synergy.Documentation.Annotations
```

Each package guide includes feature-specific examples, configuration notes, and links to the corresponding tests and their results where a file is produced.

## Examples and tests

All three Documentation projects treat compiler and NuGet warnings as errors in
Debug and Release builds. Resolve warnings rather than suppressing them when
changing these projects, including source files linked from Synergy.Catalogue.

The [`Synergy.Documentation.Tests`](Synergy.Documentation.Tests/Synergy.Documentation.Tests.csproj) project contains executable examples and snapshot tests. Run them from the repository root with:

```shell
dotnet test Documentation/Synergy.Documentation.Tests/Synergy.Documentation.Tests.csproj
```

| Feature | Example test | Result |
| --- | --- | --- |
| Public API listings | [Api.Generate](Synergy.Documentation.Tests/Architecture/Public/Api.cs) | [Documentation API](Synergy.Documentation.Tests/Architecture/Public/Api.of.Synergy.Documentation.verified.md), [Annotations API](Synergy.Documentation.Tests/Architecture/Public/Api.of.Synergy.Documentation.Annotations.verified.md) |
| API listings with assembly versions | [Package.Generate](Synergy.Documentation.Tests/Architecture/Public/Package.cs) | [Verify.Xunit API](Synergy.Documentation.Tests/Architecture/Public/Package.of.Verify.Xunit.verified.md), [xunit.core API](Synergy.Documentation.Tests/Architecture/Public/Package.of.xunit.core.verified.md) |
| Type dependencies | [Relations.Generate](Synergy.Documentation.Tests/Architecture/Dependencies/Relations.cs) | [Markdown dependency listing](Synergy.Documentation.Tests/Architecture/Dependencies/Relations.of.Markdown.verified.md) |
| TODO inventories | [Todos.Generate](Synergy.Documentation.Tests/Architecture/Debt/Todos.cs) | [Technical debt report](Synergy.Documentation.Tests/Architecture/Debt/Todos.Technical.Debt.verified.md) |
| Markdown composition | [MarkdownTests.CreateMarkdownDocument](Synergy.Documentation.Tests/Markup/MarkdownTests.cs) | [Rendered document](Synergy.Documentation.Tests/Markup/MarkdownTests.CreateMarkdownDocument.verified.md) |
| Comments as code | [NoteTests](Synergy.Documentation.Tests/Comments/NoteTests.cs) | No output file; expressions keep their original values. |
| Source file annotations | [README.Generate](../Architecture/Synergy.Architecture.Tests/Docs/README.Generate.cs), using the [annotated sample](../Architecture/Synergy.Architecture.Tests/Samples/SequenceDiagramSamples.cs) | [Architecture README with embedded sample source](../Architecture/README.md) |

The `.verified.md` files are checked-in expected results used by Verify snapshot tests.
