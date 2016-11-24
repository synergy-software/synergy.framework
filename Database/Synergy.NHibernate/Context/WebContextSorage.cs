using JetBrains.Annotations;
using Synergy.Web;

namespace Synergy.NHibernate.Context
{
    /// <summary>
    /// Contextual storage that stores object in a web context.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class WebContextSorage<T> : IWebContextSorage<T>
    {
        private readonly IHttpContextItems httpContextItems;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public WebContextSorage(IHttpContextItems httpContextItems)
        {
            this.httpContextItems = httpContextItems;
        }

        /// <inheritdoc />
        public bool IsAvailable()
        {
            return this.httpContextItems.IsAvailable();
        }

        /// <inheritdoc />
        public T Get()
        {
            string key = this.GetKey();
            return this.httpContextItems.Get<T>(key);
        }

        /// <inheritdoc />
        public void Store(T value)
        {
            string key = this.GetKey();
            this.httpContextItems.Set(key, value);
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
                this.httpContextItems.Remove(key);
            }
        }

        [NotNull]
        private string GetKey()
        {
            return typeof(T).FullName;
        }
    }

    /// <summary>
    /// Contextual storage that stores object in a web context.
    /// </summary>
    public interface IWebContextSorage<T> : IContextSorage<T>
    {
    }
}