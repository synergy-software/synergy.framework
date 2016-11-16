using JetBrains.Annotations;

namespace Synergy.NHibernate.Context
{
    public interface IContextSorage<T>
    {
        [Pure]
        bool IsAvailable();

        [CanBeNull, Pure] 
        T Get();

        void Store([NotNull] T toStore);
        void Clear();
    }
}