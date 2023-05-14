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
     include: String[]? [Nullable, Optional],
     exclude: String[]? [Nullable, Optional],
     generateAfter: Func<Scenario, bool>? [Nullable, Optional],
     callerFilePath: string [CallerFilePath, Optional]
   ) : void [Extension]
 - FeatureGenerator.Generate<TBehaviour>(
     featureClass: TBehaviour?,
     from: string,
     include: String[]? [Nullable, Optional],
     exclude: String[]? [Nullable, Optional],
     generateAfter: Func<Scenario, bool>? [Nullable, Optional],
     callerFilePath: string [CallerFilePath, Optional]
   ) : string [Extension]

## IFeature (interface)

## Scenario (record) : IEquatable<Scenario>
 - Method: string { get; }
 - Tags: ReadOnlyCollection<string> { get; set; }
 - Title: string { get; set; }
 - ctor(
     Title: string,
     Tags: ReadOnlyCollection<string>
   )
 - IsTagged(
     tag: string,
     tags: params String[] [ParamArray]
   ) : bool

