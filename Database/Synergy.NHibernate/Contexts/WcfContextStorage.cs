using System.ServiceModel;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Contexts
{
    /// <summary>
    /// Contextual storage that stores object in a WCF context.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class WcfContextStorage<T> : IWcfContextStorage<T>
    {
        /// <inheritdoc />
        public bool IsAvailable()
        {
            return OperationContext.Current != null;
        }

        /// <inheritdoc />
        public void Store(T value)
        {
            Fail.IfArgumentNull(value, nameof(value));

            this.GetWcfStorageExtension().Stored = value;
        }

        /// <inheritdoc />
        public T Get()
        {
            return this.GetWcfStorageExtension().Stored;
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
                this.GetWcfStorageExtension()
                    .Stored = default(T);
            }
        }

        [NotNull]
        private WcfOperationContextStorageExtension<T> GetWcfStorageExtension()
        {
            Fail.IfNull(OperationContext.Current, "There is no WCF " + nameof(OperationContext) + " available");

            IExtensionCollection<OperationContext> extensions = OperationContext.Current.Extensions;
            var instance = extensions.Find<WcfOperationContextStorageExtension<T>>();

            if (instance == null)
            {
                instance = new WcfOperationContextStorageExtension<T>();
                extensions.Add(instance);
            }

            return instance;
        }

        private class WcfOperationContextStorageExtension<TStored> : IExtension<OperationContext>
        {
            [CanBeNull]
            public TStored Stored { get; set; }

            public void Attach([CanBeNull] OperationContext owner)
            {
            }

            public void Detach([CanBeNull] OperationContext owner)
            {
            }
        }
    }

    /// <summary>
    /// Contextual storage that stores object in a WCF context.
    /// </summary>
    public interface IWcfContextStorage<T> : IContextStorage<T>
    {
    }
}