using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Contexts
{
    /// <summary>
    /// Contextual storage that stores object in a static field.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class StaticContextStorage<T> : IStaticContextStorage<T>
    {
        [CanBeNull] 
        private static T storedValue;

        /// <inheritdoc />
        public bool IsAvailable()
        {
            return true;
        }

        /// <inheritdoc />
        public T Get()
        {
            return StaticContextStorage<T>.storedValue;
        }

        /// <inheritdoc />
        public void Store(T value)
        {
            Fail.IfArgumentNull(value, nameof(value));

            StaticContextStorage<T>.storedValue = value;
        }

        /// <inheritdoc />
        public T Clear()
        {
            try
            {
                return this.Get();
            }
            finally
            {
                StaticContextStorage<T>.storedValue = default(T);
            }
        }
    }

    /// <summary>
    /// Contextual storage that stores object in a static field.
    /// </summary>
    public interface IStaticContextStorage<T> : IContextSorage<T>
    {
    }
}