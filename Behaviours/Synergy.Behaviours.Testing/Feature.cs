namespace Synergy.Behaviours.Testing;

public abstract class Feature<TFeature> : IFeature
{
    public virtual TFeature Given() => Self;
    public virtual TFeature When() => Self;
    public virtual TFeature Then() => Self;
    public virtual TFeature And() => Self;
    public virtual TFeature But() => Self;
    public virtual TFeature Moreover() => Self;
    protected virtual TFeature Self => (TFeature)(object)this;
}