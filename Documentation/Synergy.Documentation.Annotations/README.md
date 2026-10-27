# Synergy.Documentation.Annotations

`Synergy.Documentation.Annotations` contains lightweight source annotations and fluent helpers for making important reasoning visible next to code. It can be used on its own or together with [`Synergy.Documentation`](https://www.nuget.org/packages/Synergy.Documentation/).

**NuGet:** [Synergy.Documentation.Annotations](https://www.nuget.org/packages/Synergy.Documentation.Annotations/)

**License:** MIT

**Target frameworks:** .NET Standard 2.0 (including .NET Framework 4.6.1+), .NET 6, .NET 7, .NET 8, and .NET 10

## Install

```shell
dotnet add package Synergy.Documentation.Annotations
```

## Add explanatory comments as code

The `Note` extension methods let you attach short explanations to expressions while leaving the expression's value unchanged:

```csharp
using System.Collections.Generic;
using Synergy.Documentation.Annotations;

var capacity = 10.Because("the expected item count is known");
var items = new List<string>(capacity);

if (items.Count == 0)
    items.DoNothing("there are no items to process");
```

These helpers are intended for readability, not logging or runtime validation: they return the source value unchanged. Keep explanations focused on the reason or constraint that is not obvious from the code. Available helpers include `Comment`, `Because`, `Then`, `But`, `Therefore`, `Otherwise`, `Moreover`, `Reference`, `Satisfies`, `DoNothing`, `DoNotThrowException`, and `And`.

## Associate a type with its source file

`CodeFilePathAttribute` captures a source file path at compile time. The main `Synergy.Documentation` package can use it with `CodeFile.For<T>()` to locate the file containing a type:

```csharp
using Synergy.Documentation.Annotations;

[CodeFilePath]
public class MyDocumentedType
{
}
```

The attribute is conditional on the `DOCUMENTATION` compilation symbol. Define that symbol in the project where the annotated type is compiled if you need to retrieve its path:

```xml
<PropertyGroup>
  <DefineConstants>$(DefineConstants);DOCUMENTATION</DefineConstants>
</PropertyGroup>
```

Without that symbol, the compiler omits the attribute application, and `CodeFile.For<T>()` cannot find a source path for the type.

## Examples

The repository's [comments example](https://github.com/synergy-software/synergy.framework/blob/master/Documentation/Synergy.Documentation.Tests/Comments/NoteTests.cs) demonstrates the fluent helpers. The [Documentation package guide](https://github.com/synergy-software/synergy.framework/blob/master/Documentation/Synergy.Documentation/README.md) shows how source paths are used by its APIs.
