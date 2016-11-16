using System.ServiceModel;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Context
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class WcfContextSorage<T> : IWcfContextSorage<T>
    {
        public bool IsAvailable()
        {
            return OperationContext.Current != null;
        }

        public void Store(T value)
        {
            this.GetWcfStorageExtension().Stored = value;
        }

        public T Get()
        {
            return this.GetWcfStorageExtension().Stored;
        }

        public void Clear()
        {
            this.GetWcfStorageExtension().Stored = default(T);
        }

        [NotNull]
        private WcfOperationContextStorageExtension<T> GetWcfStorageExtension()
        {
            Fail.IfNull(OperationContext.Current, "There is no WCF OperationtionContext available");

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

            public void Attach(OperationContext owner)
            {
            }

            public void Detach(OperationContext owner)
            {
            }
        }
    }

    public interface IWcfContextSorage<T> : IContextSorage<T>
    {
    }
}