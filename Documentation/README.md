# Synergy Documentation

Synergy Documentation is a small set of .NET libraries for treating documentation as part of your codebase. Generate Markdown from public APIs, inspect type dependencies, collect TODOs, and compose Markdown documents from C#.

## Choose a package

| Package | Use it for | Documentation |
| --- | --- | --- |
| [`Synergy.Documentation`](https://www.nuget.org/packages/Synergy.Documentation/) | Generating API and dependency listings, collecting TODOs, and creating Markdown documents. | [Package guide](Synergy.Documentation/README.md) |
| [`Synergy.Documentation.Annotations`](https://www.nuget.org/packages/Synergy.Documentation.Annotations/) | Source-level annotations for locating code files and writing explanatory comments as fluent code. | [Package guide](Synergy.Documentation.Annotations/README.md) |

The main package targets .NET 6, .NET 7, .NET 8, and .NET 10. The annotations package additionally targets .NET Standard 2.0, so it can also be used from .NET Framework projects. The annotations package can be used on its own; the main package depends on it.

## Getting started

Install the package that provides the feature you need:

```shell
dotnet add package Synergy.Documentation
```

Or install only the annotations:

```shell
dotnet add package Synergy.Documentation.Annotations
```

Each package guide includes feature-specific examples, configuration notes, and links to the corresponding source samples.

## Examples and tests

The [`Synergy.Documentation.Tests`](Synergy.Documentation.Tests/Synergy.Documentation.Tests.csproj) project contains executable examples and snapshot tests. Run them from the repository root with:

```shell
dotnet test Documentation/Synergy.Documentation.Tests/Synergy.Documentation.Tests.csproj
```

The tests demonstrate [public API listings](Synergy.Documentation.Tests/Architecture/Public/Api.cs), [type dependency listings](Synergy.Documentation.Tests/Architecture/Dependencies/Relations.cs), [TODO reports](Synergy.Documentation.Tests/Architecture/Debt/Todos.cs), [comments as code](Synergy.Documentation.Tests/Comments/NoteTests.cs), and [Markdown generation](Synergy.Documentation.Tests/Markup/MarkdownTests.cs).
