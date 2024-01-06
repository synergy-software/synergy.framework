# Synergy.Documentation

## Api.ApiDescription (abstract class)
 - ApiDescription.For(
     method: MethodInfo,
     withAttributes: bool [Optional]
   ) : string
 - ApiDescription.GenerateFor(
     assembly: Assembly,
     includeAssemblyVersion: bool [Optional]
   ) : string
 - ApiDescription.GenerateFor(
     types: IEnumerable<Type>,
     description: StringBuilder? [Nullable, Optional],
     assemblyName: string? [Nullable, Optional]
   ) : string
 - ApiDescription.GetTypeName(
     method: MethodInfo
   ) : string
 - ApiDescription.GetTypeName(
     parameter: ParameterInfo
   ) : string
 - ApiDescription.GetTypeName(
     type: Type
   ) : string

## Api.ClassDocumentation (class) : Markdown+Document, IEnumerable<Markdown+IElement>, IEnumerable
 - ctor(
     type: Type
   )
 - Append(
     element: Markdown+IElement
   ) : Markdown+Document
 - Append(
     newElements: IEnumerable<Markdown+IElement>
   ) : Markdown+Document
 - GetEnumerator() : IEnumerator<Markdown+IElement>
 - ToString() : string

## Api.Dependencies (class)
 - Dependencies.Of(
     root: Type,
     includeNested: bool [Optional]
   ) : List<Type>
 - DependenciesOf(
     type: Type
   ) : IEnumerable<Type> [IteratorStateMachine]

## Code.ClassReader (class)
 - ctor()
 - ClassReader.ReadMethodBody(
     methodName: string,
     sourceFilePath: string [CallerFilePath, Optional]
   ) : string? [NullableContext]

## Code.CodeFile (class)
 - Extension: string { get; }
 - FileName: string { get; }
 - FileNameWithoutExtension: string { get; }
 - FilePath: string { get; }
 - Folder: CodeFolder { get; }
 - ctor(
     filePath: string
   )
 - CodeFile.Current(
     path: string [CallerFilePath, Optional]
   ) : CodeFile
 - CodeFile.For<T>() : CodeFile
 - ReadAllText() : string
 - RelativeTo(
     folder: CodeFolder
   ) : CodeFile
 - ToString() : string

## Code.CodeFolder (class)
 - Path: string { get; }
 - ctor(
     path: string
   )
 - CodeFolder.Current(
     path: string [CallerFilePath, Optional]
   ) : CodeFolder
 - File(
     fileName: string
   ) : CodeFile
 - Sub(
     subfolder: string
   ) : CodeFolder
 - ToString() : string
 - Up(
     jumps: int [Optional]
   ) : CodeFolder

## Markup.Markdown (class)
 - ctor()

## Markup.Markdown+Code (class) : Markdown+IElement
 - ctor(
     text: string? [Optional],
     language: string? [Optional]
   )
 - Line(
     line: string
   ) : Markdown+Code [NullableContext]
 - ToString() : string [NullableContext]

## Markup.Markdown+Document (class) : IEnumerable<Markdown+IElement>, IEnumerable
 - ctor()
 - Append(
     element: Markdown+IElement
   ) : Markdown+Document
 - Append(
     newElements: IEnumerable<Markdown+IElement>
   ) : Markdown+Document
 - GetEnumerator() : IEnumerator<Markdown+IElement>
 - ToString() : string

## Markup.Markdown+Header (abstract class) : Markdown+IElement
 - ToString() : string

## Markup.Markdown+Header1 (class) : Markdown+Header, Markdown+IElement
 - ctor(
     header: string
   )
 - ToString() : string

## Markup.Markdown+Header2 (class) : Markdown+Header, Markdown+IElement
 - ctor(
     header: string
   )
 - ToString() : string

## Markup.Markdown+Header3 (class) : Markdown+Header, Markdown+IElement
 - ctor(
     header: string
   )
 - ToString() : string

## Markup.Markdown+IElement (interface)

## Markup.Markdown+Image (class) : Markdown+IElement
 - ctor(
     filePath: CodeFile,
     alternateText: string? [Nullable, Optional]
   )
 - RelativeFrom(
     file: CodeFile
   ) : Markdown+Image
 - ToString() : string

## Markup.Markdown+Link (class) : Markdown+IElement
 - ctor(
     filePath: CodeFile,
     text: string? [Nullable, Optional]
   )
 - Link.To(
     filePath: CodeFile,
     text: string? [Nullable, Optional]
   ) : Markdown+Link
 - RelativeFrom(
     file: CodeFile
   ) : Markdown+Link
 - ToString() : string

## Markup.Markdown+Paragraph (class) : Markdown+IElement
 - ctor(
     text: string
   )
 - Line(
     line: string
   ) : Markdown+Paragraph
 - ToString() : string

## Markup.Markdown+Quote (class) : Markdown+IElement
 - ctor(
     text: string?
   )
 - Line(
     line: string
   ) : Markdown+Quote
 - ToString() : string

## Markup.Markdown+Table (class) : Markdown+IElement
 - ctor(
     headers: params String[] [ParamArray]
   )
 - Append(
     cells: params String[] [ParamArray]
   ) : void
 - ToString() : string

## Todos.Patterns.CsharpTodoPattern (record) : TodoPattern, IEquatable<TodoPattern>, IEquatable<CsharpTodoPattern>
 - FileExtension: string { get; set; }
 - Regex: Regex { get; set; }
 - TodoExtractor: Func<Match, string> { get; set; }
 - ctor()

## Todos.Patterns.GherkinTodoPattern (record) : TodoPattern, IEquatable<TodoPattern>, IEquatable<GherkinTodoPattern>
 - FileExtension: string { get; set; }
 - Regex: Regex { get; set; }
 - TodoExtractor: Func<Match, string> { get; set; }
 - ctor()

## Todos.Patterns.TextTodoPattern (record) : TodoPattern, IEquatable<TodoPattern>, IEquatable<TextTodoPattern>
 - FileExtension: string { get; set; }
 - Regex: Regex { get; set; }
 - TodoExtractor: Func<Match, string> { get; set; }
 - ctor()

## Todos.Patterns.TodoPattern (abstract class) : IEquatable<TodoPattern>
 - FileExtension: string { get; set; }
 - Regex: Regex { get; set; }
 - TodoExtractor: Func<Match, string> { get; set; }

## Todos.TodoExplorer (abstract class)
 - TodoExplorer.DebtFor(
     name: string,
     from: CodeFolder,
     currentPath: string [CallerFilePath, Optional],
     patterns: params TodoPattern[] [ParamArray]
   ) : string

