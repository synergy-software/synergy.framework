


namespace Synergy.NHibernate.Contexts
{
    //using JetBrains.Annotations;
    //using Synergy.Contracts;
    //using Synergy.Web;
    ///// <summary>
    ///// Contextual storage that stores object in a web context.
    ///// </summary>
    //[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    //public class WebContextStorage<T> : IWebContextStorage<T>
    //{
    //    private readonly IHttpContextItems httpContextItems;

    //    /// <summary>
    //    /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
    //    /// </summary>
    //    public WebContextStorage(IHttpContextItems httpContextItems)
    //    {
    //        this.httpContextItems = httpContextItems;
    //    }

    //    /// <inheritdoc />
    //    public bool IsAvailable()
    //    {
    //        return this.httpContextItems.IsAvailable();
    //    }

    //    /// <inheritdoc />
    //    public T Get()
    //    {
    //        string key = this.GetKey();
    //        return this.httpContextItems.Get<T>(key);
    //    }

    //    /// <inheritdoc />
    //    public void Store(T value)
    //    {
    //        Fail.IfArgumentNull(value, nameof(value));

    //        string key = this.GetKey();
    //        this.httpContextItems.Set(key, value);
    //    }

    //    /// <inheritdoc />
    //    public T Clear()
    //    {
    //        try
    //        {
    //            return this.Get();
    //        }
    //        finally
    //        {
    //            string key = this.GetKey();
    //            this.httpContextItems.Remove(key);
    //        }
    //    }

    //    [NotNull]
    //    private string GetKey()
    //    {
    //        return typeof(T).FullName.FailIfNull("FullName is null for {0}", typeof(T));
    //    }
    //}

    /// <summary>
    /// Contextual storage that stores object in a web context.
    /// </summary>
    public interface IWebContextStorage<T> : IContextStorage<T>
    {
    }
}