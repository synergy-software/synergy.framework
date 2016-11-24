using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Synergy.NHibernate.Context
{
    /// <summary>
    /// Contextual storage that stores object in a [ThreadStatic] field.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ThreadStaticContextSorage<T> : IThreadStaticContextSorage<T>
    {
        // TODO:mace (from:mace on:24-11-2016) refactor this class

        /// <inheritdoc />
        public bool IsAvailable()
        {
            return ThreadStaticContextScope<T>.Data != null;
        }

        /// <inheritdoc />
        public T Get()
        {
            T value;
            ThreadStaticContextScope<T>.Data.TryGetValue(typeof(T).FullName, out value);
            return value;
        }

        /// <inheritdoc />
        public void Store(T value)
        {
            ThreadStaticContextScope<T>.Data.Add(typeof(T).FullName, value);
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
                ThreadStaticContextScope<T>.Data.Clear();
            }
        }
    }

    /// <summary>
    /// Contextual storage that stores object in a [ThreadStatic] field.
    /// </summary>
    public interface IThreadStaticContextSorage<T> : IContextSorage<T>
    {
    }

    public class ThreadStaticContextScope<T> : IDisposable
    {
        [ThreadStatic]
        internal static Dictionary<string, T> Data;

        public ThreadStaticContextScope()
        {
            ThreadStaticContextScope<T>.Data = new Dictionary<string, T>(1);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (object value in ThreadStaticContextScope<T>.Data.Values)
            {
                (value as IDisposable)?.Dispose();
            }
            ThreadStaticContextScope<T>.Data.Clear();
            ThreadStaticContextScope<T>.Data = null;
        }
    }
}