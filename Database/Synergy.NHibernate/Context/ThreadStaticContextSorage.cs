using System;
using JetBrains.Annotations;

namespace Synergy.NHibernate.Context
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ThreadStaticContextSorage<T> : IThreadStaticContextSorage<T>
    {
        [ThreadStatic]
        private static T stored;

        public bool IsAvailable()
        {
            return true;
        }

        public T Get()
        {
            return ThreadStaticContextSorage<T>.stored;
        }

        public void Store(T toStore)
        {
            ThreadStaticContextSorage<T>.stored = toStore;
        }

        public void Clear()
        {
            ThreadStaticContextSorage<T>.stored = default(T);
        }
    }

    public interface IThreadStaticContextSorage<T> : IContextSorage<T>
    {
    }
}