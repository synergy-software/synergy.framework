﻿# Synergy.Behaviours.Testing

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
 - Background.Keyword: string (field)
 - ctor(
     Steps: List<Step>,
     Line: Line
   )

## Gherkin.Comment (record) : IEquatable<Comment>
 - Comment.Keyword: string (field)
 - ctor()

## Gherkin.Examples (record) : IEquatable<Examples>
 - Header: Examples+Row { get; set; }
 - Line: Line { get; set; }
 - Rows: List<Examples+Row> { get; set; }
 - Examples.Keyword: string (field)
 - ctor(
     Header: Examples+Row,
     Rows: List<Examples+Row>,
     Line: Line
   )

## Gherkin.Examples+Row (record) : IEquatable<Examples+Row>
 - Line: Line { get; set; }
 - Values: List<string> { get; set; }
 - Row.Keyword: string (field)
 - ctor(
     Values: List<string>,
     Line: Line
   )

## Gherkin.Feature (record) : IEquatable<Feature>
 - Background: Background? [Nullable] { get; set; }
 - Line: Line { get; set; }
 - Scenarios: List<Scenario> { get; set; }
 - Tags: List<string> { get; set; }
 - Title: string { get; set; }
 - Feature.Keyword: string (field)
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
 - Rule.Keyword: string (field)
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
 - Scenario.Keywords: String[] (field)
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

## Gherkin.ScenarioOutline (record) : Scenario, IEquatable<Scenario>, IEquatable<ScenarioOutline>
 - Examples: Examples { get; set; }
 - Line: Line { get; set; }
 - Rule: Rule? [Nullable] { get; set; }
 - Steps: List<Step> { get; set; }
 - Tags: List<string> { get; set; }
 - Title: string { get; set; }
 - ScenarioOutline.Keywords: String[] (field)
 - Scenario.Keywords: String[] (field)
 - ctor(
     Title: string,
     Tags: List<string>,
     Steps: List<Step>,
     Rule: Rule? [Nullable],
     Examples: Examples,
     Line: Line
   )
 - IsTagged(
     tag: string,
     tags: params String[] [ParamArray]
   ) : bool

## Gherkin.Step (record) : IEquatable<Step>
 - Step.Keywords: String[] { get; }
 - Line: Line { get; set; }
 - Text: string { get; set; }
 - Type: string { get; set; }
 - ctor(
     Type: string,
     Text: string,
     Line: Line
   )

## Gherkin.Tag (record) : IEquatable<Tag>
 - Tag.Keyword: string (field)
 - ctor()

## IFeature (interface)

