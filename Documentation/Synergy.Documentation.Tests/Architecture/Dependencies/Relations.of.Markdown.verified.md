## Synergy.Documentation.Markup.Markdown (class)
 - ctor()

## Synergy.Documentation.Markup.Markdown+IElement (interface)

## Synergy.Documentation.Markup.Markdown+Document (class) : IEnumerable<Markdown+IElement>, IEnumerable
 - ctor()
 - Append(
     element: Markdown+IElement
   ) : Markdown+Document
 - Append(
     newElements: IEnumerable<Markdown+IElement>
   ) : Markdown+Document
 - GetEnumerator() : IEnumerator<Markdown+IElement>
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Header (abstract class) : Markdown+IElement
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Header1 (class) : Markdown+Header, Markdown+IElement
 - ctor(
     header: string
   )
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Header2 (class) : Markdown+Header, Markdown+IElement
 - ctor(
     header: string
   )
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Header3 (class) : Markdown+Header, Markdown+IElement
 - ctor(
     header: string
   )
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Code (class) : Markdown+IElement
 - ctor(
     text: string? [Optional],
     language: string? [Optional]
   )
 - Line(
     line: string
   ) : Markdown+Code [NullableContext]
 - ToString() : string [NullableContext]

## Synergy.Documentation.Markup.Markdown+Paragraph (class) : Markdown+IElement
 - ctor(
     text: string
   )
 - Line(
     line: string
   ) : Markdown+Paragraph
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Quote (class) : Markdown+IElement
 - ctor(
     text: string?
   )
 - Line(
     line: string
   ) : Markdown+Quote
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Table (class) : Markdown+IElement
 - ctor(
     headers: params String[] [ParamArray]
   )
 - Append(
     cells: params String[] [ParamArray]
   ) : void
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Image (class) : Markdown+IElement
 - ctor(
     filePath: CodeFile,
     alternateText: string? [Nullable, Optional]
   )
 - RelativeTo(
     file: CodeFile
   ) : Markdown+Image
 - ToString() : string

## Synergy.Documentation.Markup.Markdown+Link (class)
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

