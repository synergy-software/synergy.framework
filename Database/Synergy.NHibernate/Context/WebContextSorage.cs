using JetBrains.Annotations;
using Synergy.Web;

namespace Synergy.NHibernate.Context
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class WebContextSorage<T> : IWebContextSorage<T>
    {
        private readonly IHttpContextItems httpContextItems;

        public WebContextSorage(IHttpContextItems httpContextItems)
        {
            this.httpContextItems = httpContextItems;
        }

        public bool IsAvailable()
        {
            return this.httpContextItems.IsAvailable();
        }

        public T Get()
        {
            string key = this.GetKey();
            return this.httpContextItems.Get<T>(key);
        }

        public void Store(T toStore)
        {
            string key = this.GetKey();
            this.httpContextItems.Set(key, toStore);
        }

        public void Clear()
        {
            string key = this.GetKey();
            this.httpContextItems.Remove(key);
        }

        [NotNull]
        private string GetKey()
        {
            return typeof(T).FullName;
        }
    }

    public interface IWebContextSorage<T> : IContextSorage<T>
    {
    }
}