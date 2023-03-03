namespace Synergy.Behaviours.Testing;

public class Feature<TFeature> : IFeature
{
    public TFeature Given() => Self;
    public TFeature When() => Self;
    public TFeature Then() => Self;
    public TFeature And() => Self;
    public TFeature But() => Self;
    public TFeature Moreover() => Self;
    protected TFeature Self => (TFeature)(object)this;
}