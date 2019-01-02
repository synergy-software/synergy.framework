// ------------------------------------------------------------------------
//
// WARN: This file is reused between Synergy projects - change it carefully
// 
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace

namespace Synergy.Core.Pooling
{
    /// <summary>
    /// Object pool for reusing objects to prevent memory fragmentation.
    /// </summary>
    /// <typeparam name="TPooled">Type of the object to be pooled</typeparam>
#if INTERNAL_POOL
    internal
#else
    public
#endif
        class Pool<TPooled>
    {
        [NotNull]
        internal readonly Func<TPooled> Constructor;

        [CanBeNull]
        internal readonly Action<TPooled> Destructor;

        [NotNull]
        private readonly Stack<Pooled<TPooled>> items;

        [NotNull]
        private readonly object syncRoot = new object();

        /// <summary>
        /// Creates a pool of objects.
        /// </summary>
        public Pool([NotNull] Func<TPooled> constructor, int initialSize = 1, [CanBeNull] Action<TPooled> destructor = null)
        {
            Fail.IfArgumentNull(constructor, nameof(constructor));

            this.Constructor = constructor;
            this.Destructor = destructor;
            this.items = new Stack<Pooled<TPooled>>(initialSize);
            for (var i = 0; i < initialSize; i++)
            {
                var pooled = new Pooled<TPooled>(this);
                this.items.Push(pooled);
            }
        }

        /// <summary>
        /// Gets the object from the pool.
        /// </summary>
        [NotNull]
        public Pooled<TPooled> Get()
        {
            lock (this.syncRoot)
            {
                if (this.items.Count == 0)
                    return new Pooled<TPooled>(this);

                return this.items.Pop();
            }
        }

        /// <summary>
        /// Returns the object to the pool.
        /// </summary>
        public void Free([NotNull] Pooled<TPooled> pooled)
        {
            Fail.IfArgumentNull(pooled, nameof(pooled));

            lock (this.syncRoot)
            {
                this.items.Push(pooled);
            }
        }
    }

    /// <summary>
    /// Pool object container. It contains the pooled object in <see cref="Value"/> property.
    /// When the object is disposed it returns to the pool it originates from.
    /// </summary>
#if INTERNAL_POOL
    internal
#else
    public
#endif
        class Pooled<TPooled> : IDisposable
    {
        [NotNull] 
        private readonly Pool<TPooled> pool;

        /// <summary>
        /// Gets the pooled object.
        /// </summary>
        [NotNull]
        public TPooled Value { get; }

        /// <summary>
        /// Creates a pooled object from the specified pool.
        /// </summary>
        public Pooled([NotNull] Pool<TPooled> pool)
        {
            Fail.IfArgumentNull(pool, nameof(pool));

            this.pool = pool;
            this.Value = pool.Constructor()
                             .OrFail(nameof(pool.Constructor));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.pool.Destructor?.Invoke(this.Value);
            this.pool.Free(this);
        }
    }
}