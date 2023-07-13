# Synergy.Architecture.Diagrams

## Sequence.SequenceDiagram (record) : IEquatable<SequenceDiagram>
 - Actor: SequenceDiagramActor [Nullable] { get; set; }
 - Components: TechnicalBlueprint+DiagramComponents? { get; set; }
 - FinishOn: Type[]? [Nullable] { get; set; }
 - FooterText: string? { get; set; }
 - Method: MethodInfo? { get; set; }
 - TitleText: string? { get; set; }
 - ctor(
     Actor: SequenceDiagramActor [Nullable],
     Method: MethodInfo? [Optional],
     FinishOn: Type[]? [Nullable, Optional],
     FooterText: string? [Optional],
     Components: TechnicalBlueprint+DiagramComponents? [Optional],
     TitleText: string? [Optional]
   )
 - Calling<T>(
     methodName: string,
     arguments: params Type[] [ParamArray]
   ) : SequenceDiagram [NullableContext]
 - Calling<T>(
     call: Expression<Action<T>>
   ) : SequenceDiagram [NullableContext]
 - Calling(
     type: Type,
     methodName: string,
     arguments: params Type[] [ParamArray]
   ) : SequenceDiagram [NullableContext]
 - Calling(
     method: MethodInfo
   ) : SequenceDiagram [NullableContext]
 - CutOff(
     types: params Type[] [ParamArray]
   ) : SequenceDiagram [NullableContext]
 - Footer(
     footer: string
   ) : SequenceDiagram [NullableContext]
 - SequenceDiagram.From<T>() : SequenceDiagram [NullableContext]
 - SequenceDiagram.From(
     actor: SequenceDiagramActor
   ) : SequenceDiagram [NullableContext]
 - Render() : string [NullableContext]
 - Title(
     title: string
   ) : SequenceDiagram [NullableContext]

## Sequence.SequenceDiagramActor (record) : IEquatable<SequenceDiagramActor>
 - Archetype: SequenceDiagramArchetype { get; set; }
 - Colour: string? { get; set; }
 - Name: string [Nullable] { get; set; }
 - Note: string? { get; set; }
 - ctor(
     Name: string [Nullable],
     Archetype: SequenceDiagramArchetype [Optional],
     Note: string? [Optional],
     Colour: string? [Optional]
   )

## Sequence.SequenceDiagramUrl (record) : IEquatable<SequenceDiagramUrl>
 - _actor: SequenceDiagramActor { get; set; }
 - _finishOn: List<Type> { get; set; }
 - _footer: string? [Nullable] { get; set; }
 - _root: MethodInfo { get; set; }
 - Components: TechnicalBlueprint+DiagramComponents { get; set; }
 - ctor(
     _actor: SequenceDiagramActor,
     _root: MethodInfo,
     _finishOn: List<Type>,
     _footer: string? [Nullable],
     Components: TechnicalBlueprint+DiagramComponents
   )
 - GenerateDiagramContent() : StringBuilder
 - GenerateDiagramUrl() : string

## Documentation.TechnicalBlueprint (class)
 - ctor()
 - Add(
     diagrams: params SequenceDiagram[] [ParamArray]
   ) : TechnicalBlueprint
 - Add(
     diagrams: IEnumerable<SequenceDiagram>
   ) : TechnicalBlueprint
 - Intro(
     markdown: string
   ) : TechnicalBlueprint
 - Register<TComponent, TImplementation>() : TechnicalBlueprint
 - Register(
     interface: Type,
     services: IServiceProvider
   ) : TechnicalBlueprint
 - Register(
     interface: Type,
     implementation: Type
   ) : TechnicalBlueprint
 - Render() : string
 - TechnicalBlueprint.Titled(
     title: string
   ) : TechnicalBlueprint
 - ToString() : string

## Documentation.TechnicalBlueprint+DiagramComponents (class)
 - ctor()
 - Register<TComponent, TImplementation>() : void [NullableContext]
 - Register(
     interface: Type,
     implementation: Type
   ) : void
 - Resolve(
     origin: Type
   ) : Type

