namespace Synergy.NHibernate.Contexts
{

    /// <summary>
    /// Contextual storage that stores object in a web context.
    /// </summary>
    public interface IWebContextStorage<T> : IContextStorage<T>
    {
    }
}