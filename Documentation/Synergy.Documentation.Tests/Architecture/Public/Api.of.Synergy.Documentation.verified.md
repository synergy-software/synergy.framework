# Synergy.Documentation

## Todos.TodoExplorer (abstract class)
 - TodoExplorer.DebtFor(
     name: string,
     from: CodeFolder,
     currentPath: string [CallerFilePath, Optional],
     patterns: params TodoPattern[] [ParamArray]
   ) : string

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
   ) : CodeFolder
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
 - ToString() : string
 - Up(
     jumps: int [Optional]
   ) : CodeFolder

## Api.ApiDescription (abstract class)
 - ApiDescription.For(
     method: MethodInfo,
     withAttributes: bool [Optional]
   ) : string
 - ApiDescription.GenerateFor(
     assembly: Assembly
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

