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
        private static T value;

        /// <inheritdoc />
        public bool IsAvailable()
        {
            return true;
        }

        /// <inheritdoc />
        public T Get()
        {
            return StaticContextStorage<T>.value;
        }

        /// <inheritdoc />
        public void Store(T value)
        {
            Fail.IfArgumentNull(value, nameof(value));

            StaticContextStorage<T>.value = value;
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
                StaticContextStorage<T>.value = default(T);
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