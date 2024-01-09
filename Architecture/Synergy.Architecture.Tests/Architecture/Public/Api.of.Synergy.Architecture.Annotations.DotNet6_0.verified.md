# Synergy.Architecture.Annotations

## Diagrams.Sequence.SequenceDiagramActivationAttribute (attribute) : Attribute, SequenceDiagramElement, SequenceDiagramGroup
 - Archetype: SequenceDiagramArchetype { get; set; }
 - Group: SequenceDiagramGroupType { get; set; }
 - GroupHeader: string? { get; set; }
 - Note: string? { get; set; }
 - Type: Type [Nullable] { get; }
 - ctor(
     type: Type
   )

## Diagrams.Sequence.SequenceDiagramArchetype (enum) : IComparable, IFormattable, IConvertible
 - Participant = 0
 - Actor = 1
 - Boundary = 2
 - Control = 3
 - Entity = 4
 - Database = 5
 - Collections = 6
 - Queue = 7

## Diagrams.Sequence.SequenceDiagramCallAttribute (attribute) : Attribute, SequenceDiagramElement, SequenceDiagramGroup
 - Archetype: SequenceDiagramArchetype { get; set; }
 - Group: SequenceDiagramGroupType { get; set; }
 - GroupHeader: string? { get; set; }
 - Message: string? { get; set; }
 - Method: string [Nullable] { get; }
 - Note: string? { get; set; }
 - Result: string? { get; set; }
 - Type: Type [Nullable] { get; }
 - ctor(
     type: Type,
     method: string
   )

## Diagrams.Sequence.SequenceDiagramDatabaseCallAttribute (attribute) : SequenceDiagramExternalCallAttribute, SequenceDiagramElement, SequenceDiagramGroup
 - Archetype: SequenceDiagramArchetype { get; set; }
 - Group: SequenceDiagramGroupType { get; set; }
 - GroupHeader: string? { get; set; }
 - Method: string [Nullable] { get; }
 - Note: string? { get; set; }
 - Result: string { get; }
 - Type: string [Nullable] { get; }
 - ctor(
     sql: string
   )

## Diagrams.Sequence.SequenceDiagramDeactivationAttribute (attribute) : Attribute, SequenceDiagramElement
 - Note: string? [Nullable] { get; set; }
 - Type: Type { get; }
 - ctor(
     type: Type
   )

## Diagrams.Sequence.SequenceDiagramElement (interface)

## Diagrams.Sequence.SequenceDiagramElementAttribute (attribute) : Attribute, SequenceDiagramElement
 - Archetype: SequenceDiagramArchetype { get; set; }
 - Colour: string? { get; set; }
 - Note: string? { get; set; }
 - ctor()

## Diagrams.Sequence.SequenceDiagramExternalActivationAttribute (attribute) : Attribute, SequenceDiagramElement, SequenceDiagramGroup
 - Archetype: SequenceDiagramArchetype { get; set; }
 - Constructor: string { get; }
 - Group: SequenceDiagramGroupType { get; set; }
 - GroupHeader: string? { get; set; }
 - Note: string? { get; set; }
 - Type: string [Nullable] { get; }
 - ctor(
     type: string,
     constructor: string? [Nullable]
   )

## Diagrams.Sequence.SequenceDiagramExternalCallAttribute (attribute) : Attribute, SequenceDiagramElement, SequenceDiagramGroup
 - Archetype: SequenceDiagramArchetype { get; set; }
 - Group: SequenceDiagramGroupType { get; set; }
 - GroupHeader: string? { get; set; }
 - Method: string [Nullable] { get; }
 - Note: string? { get; set; }
 - Result: string { get; }
 - Type: string [Nullable] { get; }
 - ctor(
     type: string,
     method: string,
     result: string? [Nullable, Optional]
   )

## Diagrams.Sequence.SequenceDiagramGroup (interface)
 - Group: SequenceDiagramGroupType { get; }
 - GroupHeader: string { get; }

## Diagrams.Sequence.SequenceDiagramGroupType (enum) : IComparable, IFormattable, IConvertible
 - None = 0
 - Alt = 1
 - Else = 2
 - Loop = 3
 - Par = 4
 - Break = 5
 - Critical = 6
 - Group = 7

## Diagrams.Sequence.SequenceDiagramNoteAttribute (attribute) : Attribute, SequenceDiagramElement
 - Note: string? { get; set; }
 - ctor(
     note: string
   )

## Diagrams.Sequence.SequenceDiagramSelfCallAttribute (attribute) : Attribute, SequenceDiagramElement, SequenceDiagramGroup
 - Group: SequenceDiagramGroupType { get; set; }
 - GroupHeader: string? { get; set; }
 - Method: string [Nullable] { get; }
 - Note: string? { get; set; }
 - ctor(
     method: string
   )

