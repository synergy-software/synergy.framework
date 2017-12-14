using System;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Contexts
{
    /// <summary>
    ///     Contextual storage that stores object in a [ThreadStatic] field.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ThreadStaticContextSorage<T> : IThreadStaticContextSorage<T>
    {
        /// <inheritdoc />
        public bool IsAvailable()
        {
            return ThreadStaticContextScope<T>.Sack != null;
        }

        /// <inheritdoc />
        public T Get()
        {
            this.FailIfThreadStaticContextScopeNotAvailable();

            return ThreadStaticContextScope<T>.Sack.Value;
        }

        /// <inheritdoc />
        public void Store(T value)
        {
            Fail.IfArgumentNull(value, nameof(value));
            this.FailIfThreadStaticContextScopeNotAvailable();

            ThreadStaticContextScope<T>.Sack.Value = value;
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
                ThreadStaticContextScope<T>.Sack.Value = default(T);
            }
        }

        private void FailIfThreadStaticContextScopeNotAvailable()
        {
            Fail.IfFalse(this.IsAvailable(), nameof(ThreadStaticContextScope<T>) + " is not available");
        }
    }

    /// <summary>
    ///     Contextual storage that stores object in a [ThreadStatic] field.
    /// </summary>
    public interface IThreadStaticContextSorage<T> : IContextSorage<T>
    {
    }

    public abstract class ThreadStaticContextScope<T> : IDisposable
    {
        [ThreadStatic]
        internal static SackOf<T> Sack;

        protected ThreadStaticContextScope()
        {
            // WARN: nested scopes are not supported as it it to erroneous
            Fail.IfNotNull(ThreadStaticContextScope<T>.Sack, nameof(ThreadStaticContextScope<T>) + " was not cleared properly - are you trying to nest the scope? It is forbidden.");

            ThreadStaticContextScope<T>.Sack = new SackOf<T>();
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            Fail.IfNull(ThreadStaticContextScope<T>.Sack, nameof(ThreadStaticContextScope<T>.Sack) + " is null");
            ThreadStaticContextScope<T>.Sack.Value = default(T);
            ThreadStaticContextScope<T>.Sack = null;
        }

        internal class SackOf<T>
        {
            [CanBeNull]
            public T Value { get; set; }
        }
    }
}