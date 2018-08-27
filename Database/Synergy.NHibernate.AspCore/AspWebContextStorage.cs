using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Synergy.Contracts;
using Synergy.NHibernate.Contexts;

namespace Synergy.NHibernate.AspCore
{
    /// <summary>
    /// Contextual storage that stores object in a web context.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class WebContextStorage<T> : IWebContextStorage<T>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public WebContextStorage(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public bool IsAvailable()
        {
            return this.httpContextAccessor.HttpContext != null;
        }

        /// <inheritdoc />
        public T Get()
        {
            string key = this.GetKey();
            return (T)this.httpContextAccessor.HttpContext.Items[key];
        }

        /// <inheritdoc />
        public void Store(T value)
        {
            Fail.IfArgumentNull(value, nameof(value));

            string key = this.GetKey();
            this.httpContextAccessor.HttpContext.Items[key]= value;
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
                string key = this.GetKey();
                this.httpContextAccessor.HttpContext.Items.Remove(key);
            }
        }

        [NotNull]
        private string GetKey()
        {
            return typeof(T).FullName.FailIfNull("FullName is null for {0}", typeof(T));
        }
    }
}
