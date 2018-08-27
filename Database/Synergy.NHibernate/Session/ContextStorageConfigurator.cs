using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Synergy.NHibernate.Contexts;

namespace Synergy.NHibernate.Session
{
    public class ContextStorageConfigurator
    {
        private List<Type> StorageTypes { get; set; } = new List<Type>();

        public void Add<T>()
            where T : IContextStorage<SessionsContainer>
        {
            this.StorageTypes.Add(typeof(T));
        }

        [NotNull, Pure]
        internal Type[] GetStorageTypes()
        {
            return this.StorageTypes.ToArray();
        }
    }
}