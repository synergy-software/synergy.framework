# Synergy.Behaviours.Testing

## Feature<TFeature> (abstract class) : IFeature
 - And() : TFeature?
 - Background() : TFeature?
 - But() : TFeature?
 - Given() : TFeature?
 - Moreover() : TFeature?
 - Then() : TFeature?
 - When() : TFeature?

## FeatureGenerator (abstract class)
 - FeatureGenerator.Generate<TBehaviour>(
     feature: TBehaviour?,
     from: string,
     to: string,
     include: Func<Scenario, bool>? [Nullable, Optional],
     generateAfter: Func<Scenario, bool>? [Nullable, Optional],
     callerFilePath: string [CallerFilePath, Optional]
   ) : void [Extension]
 - FeatureGenerator.Generate<TBehaviour>(
     featureClass: TBehaviour?,
     from: string,
     include: Func<Scenario, bool>? [Nullable, Optional],
     generateAfter: Func<Scenario, bool>? [Nullable, Optional],
     callerFilePath: string [CallerFilePath, Optional]
   ) : string [Extension]

## Gherkin.Background (record) : IEquatable<Background>
 - Line: Line { get; set; }
 - Steps: List<Step> { get; set; }
 - ctor(
     Steps: List<Step>,
     Line: Line
   )

## Gherkin.Feature (record) : IEquatable<Feature>
 - Background: Background? [Nullable] { get; set; }
 - Line: Line { get; set; }
 - Scenarios: List<Scenario> { get; set; }
 - Tags: List<string> { get; set; }
 - Title: string { get; set; }
 - ctor(
     Title: string,
     Tags: List<string>,
     Background: Background? [Nullable],
     Scenarios: List<Scenario>,
     Line: Line
   )

## Gherkin.Line (record) : IEquatable<Line>
 - Number: int { get; set; }
 - Text: string { get; set; }
 - ctor(
     Number: int,
     Text: string
   )

## Gherkin.Rule (record) : IEquatable<Rule>
 - Background: Background? [Nullable] { get; set; }
 - Line: Line { get; set; }
 - Title: string { get; set; }
 - ctor(
     Title: string,
     Background: Background? [Nullable],
     Line: Line
   )

## Gherkin.Scenario (record) : IEquatable<Scenario>
 - Line: Line { get; set; }
 - Rule: Rule? [Nullable] { get; set; }
 - Steps: List<Step> { get; set; }
 - Tags: List<string> { get; set; }
 - Title: string { get; set; }
 - ctor(
     Title: string,
     Tags: List<string>,
     Steps: List<Step>,
     Rule: Rule? [Nullable],
     Line: Line
   )
 - IsTagged(
     tag: string,
     tags: params String[] [ParamArray]
   ) : bool

## Gherkin.Step (record) : IEquatable<Step>
 - Line: Line { get; set; }
 - Text: string { get; set; }
 - Type: string { get; set; }
 - ctor(
     Type: string,
     Text: string,
     Line: Line
   )

## IFeature (interface)

