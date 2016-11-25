using JetBrains.Annotations;

namespace Synergy.NHibernate.Contexts
{
    /// <summary>
    /// Base interface of all contextual storages.
    /// Contextual storage allows you to store and retrieve objects from a specific place 
    /// (e.g. web context, web session, static field, etc.)
    /// </summary>
    public interface IContextSorage<T>
    {
        /// <summary>
        /// Informs whethee this particular storage is available at the moment.
        /// </summary>
        [Pure]
        bool IsAvailable();

        /// <summary>
        /// Gets an object stored in this particular context.
        /// If there is nothing stored it will return null.
        /// </summary>
        [CanBeNull, Pure] 
        T Get();

        /// <summary>
        /// Stores an object in this particular context.
        /// </summary>
        void Store([NotNull] T value);

        /// <summary>
        /// Clears the context - removes whatever was stored in this context.
        /// </summary>
        [CanBeNull] 
        T Clear();
    }
}